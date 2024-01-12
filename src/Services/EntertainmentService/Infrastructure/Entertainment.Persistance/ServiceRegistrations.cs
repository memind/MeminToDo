using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using OpenTracing;
using Common.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Logging.Handlers;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;
using MassTransit;
using Common.Messaging.MassTransit.Services.ApiServices.BackUp;
using Common.Messaging.MassTransit.Services.ApiServices.Test;
using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Services.ApiServices.Connected;
using Common.Messaging.RabbitMQ.Configurations;

namespace Entertainment.Persistance
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg, IHostBuilder host)
        {
            #region HealthChecks
            services.AddHealthChecks().AddNpgSql(cfg.GetConnectionString("PostgreSql"));
            #endregion

            #region IdentityServer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:8005";
                    options.Audience = "Entertainment";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(authOption =>
            {
                authOption.AddPolicy("EntertainmentRead", policy => policy.RequireClaim("scope", "Entertainment.Read"));
                authOption.AddPolicy("EntertainmentWrite", policy => policy.RequireClaim("scope", "Entertainment.Write"));
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

            #region SeriLog
            host.UseSerilog(SeriLogger.Configure);
            services.AddTransient<LoggingDelegatingHandler>();
            #endregion

            #region MassTransit
            services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<ConsumeTestMessageService>();
                configurator.AddConsumer<ConsumeBackUpMessageService>();

                configurator.UsingRabbitMq((context, _configurator) =>
                {
                    _configurator.Host(cfg.GetSection("RabbitMqHost").Value);

                    _configurator.ReceiveEndpoint(MessagingConsts.StartTestQueue(), e => e.ConfigureConsumer<ConsumeTestMessageService>(context));
                    _configurator.ReceiveEndpoint(MessagingConsts.BackUpQueue(), e => e.ConfigureConsumer<ConsumeBackUpMessageService>(context));
                });
            });

            services.AddHostedService<PublishConnectedMessageService>(provider =>
            {
                using IServiceScope scope = provider.CreateScope();
                IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

                return new(publishEndpoint, cfg.GetSection("ServiceName").Value);
            });
            #endregion

            #region RabbitMQ
            services.AddScoped<IMessageConsumerService, MessageConsumerService>();
            services.Configure<RabbitMqUri>(cfg.GetSection("RabbitMqHost"));
            #endregion

            return services;
        }
    }
}
