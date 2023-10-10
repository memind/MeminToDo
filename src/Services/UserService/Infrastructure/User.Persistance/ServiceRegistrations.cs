using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Persistance.IdentityConfiguration;

namespace User.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddIdentityServer()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetTestUsers().ToList())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddDeveloperSigningCredential();
            return services;
        }
    }
}
