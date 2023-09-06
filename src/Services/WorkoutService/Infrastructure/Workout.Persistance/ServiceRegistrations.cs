using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Application.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using Workout.Persistance.Concretes.Repositories;
using Workout.Persistance.Context;

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
