using Meal.Infrastructure.Context;
using Meal.Infrastructure.Repositories.Abstract;
using Meal.Infrastructure.Repositories.Concrete;
using uow = Meal.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger;
using Microsoft.Extensions.Logging;
using OpenTracing.Util;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using Common.Logging;
using Serilog;

namespace Meal.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            services.AddDbContext<MealDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MsSqlDatabaseConnectionString")));

            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(uow.IUnitOfWork), typeof(uow.UnitOfWork));

            #region OpenTracing/Jaeger
            services.AddSingleton<ITracer>(sp =>
            {
                var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender("host.docker.internal", 6831, 0))
                    .Build();
                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .WithReporter(reporter)
                    .Build();
                if (!GlobalTracer.IsRegistered())
                {
                    GlobalTracer.Register(tracer);
                }
                return tracer;
            });

            services.AddOpenTracing();

            services.Configure<HttpHandlerDiagnosticOptions>(options =>
                options.OperationNameResolver =
                    request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");

            services.AddHttpClient();


            services.Configure<AspNetCoreDiagnosticOptions>(options =>
            {
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/status"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/metrics"));
            });
            #endregion

            #region Logging
            host.UseSerilog(SeriLogger.Configure);
            services.AddTransient<LoggingDelegatingHandler>();
            #endregion

            #region HealthCheck
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("MsSqlDatabaseConnectionString"));
            #endregion

            #region Appmetrics - Prometheus - Grafana
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddMetrics();

            host.UseMetricsWebTracking()
                            .UseMetrics(options =>
                            {
                                options.EndpointOptions = endpointsOptions =>
                                {
                                    endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                                    endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                                    endpointsOptions.EnvironmentInfoEndpointEnabled = false;
                                };
                            });

            services.AddMvcCore().AddMetricsCore();
            #endregion

            return services;
        }
    }
}
