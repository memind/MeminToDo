using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Entertainment.Persistance
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddHealthChecks().AddNpgSql(cfg.GetConnectionString("PostgreSql"));
            return services;
        }
    }
}
