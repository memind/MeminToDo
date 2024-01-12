using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using OpenTracing;
using Skill.Application.Abstractions.Services;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Application.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Repositories.ArtRepositories;
using Skill.Persistance.Concretes.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Services;
using Skill.Persistance.Context;
using Skill.Persistance.DependencyResolver.Autofac;
using Microsoft.Extensions.Logging;
using Common.Logging;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Common.Logging.Handlers;
using Amazon.S3;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;
using Common.Messaging.MassTransit.Services.ApiServices.Connected;
using MassTransit;
using Common.Messaging.MassTransit.Services.ApiServices.Test;
using Common.Messaging.MassTransit.Services.ApiServices.BackUp;
using Common.Messaging.MassTransit.Consts;
using Common.Messaging.RabbitMQ.Configurations;

namespace Skill.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg, IHostBuilder host)
        {
            #region Database
            services.Configure<MongoSettings>(opt =>
            {
                opt.ConnectionString = cfg.GetSection("MongoConnection:ConnectionString").Value;
                opt.Database = cfg.GetSection("MongoConnection:Database").Value;
            });
            #endregion

            #region IdentityServer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "http://localhost:8005";
                    options.Audience = "Skill";
                    options.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(authOption =>
            {
                authOption.AddPolicy("SkillRead", policy => policy.RequireClaim("scope", "Skill.Read"));
                authOption.AddPolicy("SkillWrite", policy => policy.RequireClaim("scope", "Skill.Write"));
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

            #region Serilog
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

            #region AWS
            services.AddAWSService<IAmazonS3>();
            #endregion

            #region HealthCheck
            services.AddHealthChecks().AddMongoDb(mongodbConnectionString: cfg.GetSection("MongoConnection:ConnectionString").Value, mongoDatabaseName: cfg.GetSection("MongoConnection:Database").Value);
            #endregion

            #region Project Services
            services.AddSingleton<IFileService, FileService>();
            #endregion

            return services;
        }
    }
}
