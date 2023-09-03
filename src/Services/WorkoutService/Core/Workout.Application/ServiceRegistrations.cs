using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Workout.Application
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}
