using Microsoft.Extensions.DependencyInjection;

namespace Meal.Mapper
{
    public static class ServiceRegistration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<ICustomMapper, Mapper>();
        }
    }
}
