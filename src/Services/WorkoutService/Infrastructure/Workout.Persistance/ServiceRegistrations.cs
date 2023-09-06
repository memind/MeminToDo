using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using Workout.Persistance.Concretes.Repositories;
using Workout.Persistance.Context;

namespace Workout.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<WorkoutDbContext>(options => options.UseCosmos("", databaseName: ""));


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
