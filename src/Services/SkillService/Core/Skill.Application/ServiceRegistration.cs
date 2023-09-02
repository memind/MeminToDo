using Microsoft.Extensions.DependencyInjection;
using Skill.Application.Features.Queries.ArtQueries.GetAllArts;
using System.Reflection;

namespace Skill.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllArtsQueryRequest>());

            return services;
        }
    }
}
