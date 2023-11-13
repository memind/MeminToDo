using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Persistance.IdentityConfiguration;

namespace User.Persistance.SeedData
{
    public static class Seeder
    {
        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();

            services.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString);
            });

            services.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString);
            });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();

                context.Database.Migrate();
                EnsureSeedData(context);
            }
        }
        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in Config.GetClients().ToList())
                {
                    IdentityServer4.EntityFramework.Entities.Client mappingClient = new IdentityServer4.EntityFramework.Entities.Client();
                    mappingClient.ClientId = client.ClientId;
                    mappingClient.ClientName = client.ClientName;

                    mappingClient.ClientSecrets = new List<ClientSecret>();
                    foreach (var clientSecret in client.ClientSecrets)
                    {
                        mappingClient.ClientSecrets.Add(new()
                        {
                            Value = clientSecret.Value,
                            Expiration = clientSecret.Expiration,
                            Description = clientSecret.Description,
                            Type = clientSecret.Type
                        });
                    };

                    mappingClient.AllowedGrantTypes = new List<ClientGrantType>();
                    foreach (var grantType in client.AllowedGrantTypes)
                    {
                        mappingClient.AllowedGrantTypes.Add(new()
                        {
                            GrantType = grantType
                        });
                    }

                    mappingClient.AllowedScopes = new List<ClientScope>();
                    foreach (var scope in client.AllowedScopes)
                    {
                        mappingClient.AllowedScopes.Add(new()
                        {
                            Scope = scope
                        });
                    }

                    context.Clients.Add(mappingClient);
                }
                context.SaveChanges();
            }
            else
                Console.WriteLine("Clients already populated");
            

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in Config.GetIdentityResources().ToList())
                {
                    IdentityServer4.EntityFramework.Entities.IdentityResource mappingIdentityResource = new IdentityServer4.EntityFramework.Entities.IdentityResource();

                    mappingIdentityResource.Name = resource.Name;
                    mappingIdentityResource.DisplayName = resource.DisplayName;

                    mappingIdentityResource.UserClaims = new List<IdentityResourceClaim>();

                    foreach (var claim in resource.UserClaims)
                        mappingIdentityResource.UserClaims.Add( new() { Type = claim });
                    
                    context.IdentityResources.Add(mappingIdentityResource);
                }
                context.SaveChanges();
            }
            else
                Console.WriteLine("IdentityResources already populated");
            

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in Config.GetApiResources().ToList())
                {
                    IdentityServer4.EntityFramework.Entities.ApiResource mappingApiResource = new IdentityServer4.EntityFramework.Entities.ApiResource();

                    mappingApiResource.Name = resource.Name;

                    mappingApiResource.Secrets = new List<ApiResourceSecret>();
                    foreach (var secret in resource.ApiSecrets)
                        mappingApiResource.Secrets.Add(new()
                        {
                            Value = secret.Value,
                            Type = secret.Type,
                            Expiration = secret.Expiration,
                            Description = secret.Description
                        });

                    mappingApiResource.Scopes = new List<ApiResourceScope>();
                    foreach (var scope in resource.Scopes)
                        mappingApiResource.Scopes.Add(new() { Scope = scope });
                    
                    
                    context.ApiResources.Add(mappingApiResource);
                }
                context.SaveChanges();
            }
            else
                Console.WriteLine("ApiResources already populated");
            

            if (!context.ApiScopes.Any())
            {
                Console.WriteLine("Scopes being populated");
                foreach (var scope in Config.GetApiScopes().ToList())
                    context.ApiScopes.Add(new() { Name = scope.Name, DisplayName = scope.DisplayName});
                
                context.SaveChanges();
            }
            else
                Console.WriteLine("Scopes already populated");
            

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }
    }
}
