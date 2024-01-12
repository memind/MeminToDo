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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Meal.Infrastructure.ExecutionStrategies;
using Common.Logging.Handlers;
using MassTransit;
using Common.Messaging.MassTransit.Services.ApiServices.Test;
using Common.Messaging.MassTransit.Services.ApiServices.BackUp;
using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Services.ApiServices.Connected;
using Common.Messaging.RabbitMQ.Configurations;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;

namespace Meal.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            #region Database
            services.AddDbContext<MealDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MsSqlDatabaseConnectionString"), 
                    builder => builder.ExecutionStrategy(dependencies => new EFCoreCustomRetryExecutionStrategy(
                                                                             dependencies: dependencies, 
                                                                             maxRetryCount: 4,
                                                                             maxRetryDelay: TimeSpan.FromSeconds(30)))));
            #endregion

            #region Project Services
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));

            services.AddScoped(typeof(uow.IUnitOfWork), typeof(uow.UnitOfWork));
            #endregion

            #region RabbitMQ
            services.Configure<RabbitMqUri>(configuration.GetSection("RabbitMqHost"));
            services.AddScoped<IMessageConsumerService, MessageConsumerService>();
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

            #region Logging
            host.UseSerilog(SeriLogger.Configure);
            services.AddTransient<LoggingDelegatingHandler>();
            #endregion

            #region HealthCheck
            services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("MsSqlDatabaseConnectionString"));
            #endregion

            #region MassTransit
            services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<ConsumeTestMessageService>();
                configurator.AddConsumer<ConsumeBackUpMessageService>();

                configurator.UsingRabbitMq((context, _configurator) =>
                {
                    _configurator.Host(configuration.GetSection("RabbitMqHost").Value);

                    _configurator.ReceiveEndpoint(MessagingConsts.StartTestQueue(), e => e.ConfigureConsumer<ConsumeTestMessageService>(context));
                    _configurator.ReceiveEndpoint(MessagingConsts.BackUpQueue(), e => e.ConfigureConsumer<ConsumeBackUpMessageService>(context));
                });
            });

            services.AddHostedService<PublishConnectedMessageService>(provider =>
            {
                using IServiceScope scope = provider.CreateScope();
                IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

                return new(publishEndpoint, configuration.GetSection("ServiceName").Value);
            });
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

            #region IdentityServer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:8005";
                    options.Audience = "Meal";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(authOption =>
            {
                authOption.AddPolicy("MealRead", policy => policy.RequireClaim("scope", "Meal.Read"));
                authOption.AddPolicy("MealWrite", policy => policy.RequireClaim("scope", "Meal.Write"));
            });
            #endregion

            return services;
        }
    }
}
