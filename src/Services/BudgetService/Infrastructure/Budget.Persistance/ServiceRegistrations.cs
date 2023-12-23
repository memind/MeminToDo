using Budget.Application.Repositories;
using Budget.Persistance.Concretes.Repositories;
using uow = Budget.Persistance.UnitOfWork;
using Common.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Budget.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Budget.Persistance.Context;
using Budget.Persistance.Concretes.Factories;
using Budget.Application.Abstractions.Factories;
using Budget.Application.Abstractions.Services;
using Budget.Persistance.Concretes.Services;
using Common.Logging.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using App.Metrics.Formatters.Prometheus;
using OpenTracing;
using Jaeger.Reporters;
using Jaeger.Senders.Thrift;
using Jaeger;
using Jaeger.Samplers;
using OpenTracing.Util;
using OpenTracing.Contrib.NetCore.Configuration;
using Budget.Application.Abstractions.Hubs;
using Budget.Persistance.SignalR.HubService;

namespace Budget.Persistance
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg, IHostBuilder host)
        {
            #region IdentityServer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:8005";
                    options.Audience = "Budget";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(authOption =>
            {
                authOption.AddPolicy("BudgetRead", policy => policy.RequireClaim("scope", "Budget.Read"));
                authOption.AddPolicy("BudgetWrite", policy => policy.RequireClaim("scope", "Budget.Write"));
            });
            #endregion

            services.AddDbContext<BudgetDbContext>(options =>
                options.UseSqlServer(cfg.GetConnectionString("MsSqlDatabaseConnectionString")).EnableSensitiveDataLogging());

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

            #region HealthCheck
            services.AddHealthChecks().AddSqlServer(cfg.GetConnectionString("MsSqlDatabaseConnectionString"));
            #endregion

            #region Serilog
            host.UseSerilog(SeriLogger.Configure);
            services.AddTransient<LoggingDelegatingHandler>();
            #endregion

            #region ProjectServices
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(IUnitOfWork), typeof(uow.UnitOfWork));

            services.AddScoped(typeof(IMoneyFlowFactory), typeof(MoneyFlowFactory));

            services.AddScoped(typeof(IMoneyFlowService), typeof(MoneyFlowService));
            services.AddScoped(typeof(IBudgetAccountService), typeof(BudgetAccountService));
            services.AddScoped(typeof(IWalletService), typeof(WalletService));
            #endregion

            return services;
        }

        public static IServiceCollection AddSignalRServices(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddTransient(typeof(IPriceHubService), typeof(PriceHubService));

            return services;
        }
    }
}


