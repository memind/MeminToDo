using HomePages.Helpers;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using User.Application.Abstractions.Repositories;
using User.Domain.Entities;
using User.Persistance.Concretes.Repositories;
using User.Persistance.Concretes.Services;
using User.Persistance.Context;
using User.Persistance.IdentityConfiguration;
using User.Persistance.SeedData;
using static System.Formats.Asn1.AsnWriter;

namespace User.Persistance
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration cfg)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(cfg.GetConnectionString("MsSqlConnectionString"), action =>
                {
                    action.MigrationsAssembly(assemblyName);
                });
            });

            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

            services.AddIdentityServer()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryApiScopes(Config.GetApiScopes())
                    .AddInMemoryClients(Config.GetClients())
                    .AddTestUsers(Config.GetTestUsers().ToList())
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddAspNetIdentity<AppUser>()
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    .AddProfileService<CustomProfileService>()
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = context =>
                        {
                            context.UseSqlServer(cfg.GetConnectionString("MsSqlConnectionString"), action =>
                            {
                                action.MigrationsAssembly(assemblyName);
                            });
                        };
                    })
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = context =>
                        {
                            context.UseSqlServer(cfg.GetConnectionString("MsSqlConnectionString"), action =>
                            {
                                action.MigrationsAssembly(assemblyName);
                            });
                        };
                    })
                    .AddDeveloperSigningCredential();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAuthentication();
            Seeder.EnsureSeedData(cfg.GetConnectionString("MsSqlConnectionString"));

            return services;
        }
    }
}
