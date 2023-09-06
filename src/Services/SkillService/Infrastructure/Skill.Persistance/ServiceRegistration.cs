using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.Configure<MongoSettings>(opt =>
            {
                opt.ConnectionString = cfg.GetSection("MongoConnection:ConnectionString").Value;
                opt.Database = cfg.GetSection("MongoConnection:Database").Value;
            });

            services.AddHealthChecks().AddMongoDb(mongodbConnectionString: cfg.GetSection("MongoConnection:ConnectionString").Value, mongoDatabaseName: cfg.GetSection("MongoConnection:Database").Value);

            return services;
        }
    }
}
