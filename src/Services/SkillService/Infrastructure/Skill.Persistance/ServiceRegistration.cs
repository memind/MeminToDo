using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Skill.Application.Abstractions.Services;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Application.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Repositories.ArtRepositories;
using Skill.Persistance.Concretes.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Services;
using Skill.Persistance.Context;
using Skill.Persistance.DependencyResolver.Autofac;

namespace Skill.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg, IHostBuilder host)
        {
            services.Configure<MongoSettings>(opt =>
            {
                opt.ConnectionString = cfg.GetSection("MongoConnection:ConnectionString").Value;
                opt.Database = cfg.GetSection("MongoConnection:Database").Value;
            });

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

            services.AddHealthChecks().AddMongoDb(mongodbConnectionString: cfg.GetSection("MongoConnection:ConnectionString").Value, mongoDatabaseName: cfg.GetSection("MongoConnection:Database").Value);

            return services;
        }
    }
}
