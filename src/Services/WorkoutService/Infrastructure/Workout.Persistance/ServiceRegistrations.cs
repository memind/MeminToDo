using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Application.Repositories;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing;
using System;
using Workout.Persistance.Concretes.Repositories;
using Workout.Persistance.Context;
using Autofac.Core;
using OpenTracing.Util;

namespace Workout.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IHostBuilder host)
        {
            services.AddDbContext<WorkoutDbContext>(options => options.UseCosmos("", databaseName: ""));

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

            services.AddHealthChecks().AddDbContextCheck<WorkoutDbContext>("WorkoutDB Health Check", HealthStatus.Degraded, customTestQuery: PerformCosmosHealthCheck());
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            return services;
        }

        private static Func<WorkoutDbContext, CancellationToken, Task<bool>> PerformCosmosHealthCheck() =>
            async (context, _) =>
            {
                try
                {
                    await context.Database.GetCosmosClient().ReadAccountAsync().ConfigureAwait(false);
                }
                catch (HttpRequestException)
                {
                    return false;
                }
                return true;
            };
    }
}
