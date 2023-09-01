using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skill.Application.Abstractions.Services;
using Skill.Application.Repositories.ArtRepositories;
using Skill.Application.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Repositories.ArtRepositories;
using Skill.Persistance.Concretes.Repositories.SongRepositories;
using Skill.Persistance.Concretes.Services;
using Skill.Persistance.Context;

namespace Skill.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.Configure<MongoSettings>(opt =>
            {
                opt.ConnectionString = cfg.GetSection("MongoConnection:ConnectionString").Value;
                opt.Database = cfg.GetSection("MongoConnection:Database").Value;
            });
            services.AddScoped<IArtReadRepository, ArtReadRepository>();
            services.AddScoped<IArtWriteRepository, ArtWriteRepository>();

            services.AddScoped<ISongReadRepository, SongReadRepository>();
            services.AddScoped<ISongWriteRepository, SongWriteRepository>();

            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IArtService, ArtService>();

            return services;
        }
    }
}
