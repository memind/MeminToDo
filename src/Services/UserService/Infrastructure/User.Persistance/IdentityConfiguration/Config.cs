using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace User.Persistance.IdentityConfiguration
{
    static public class Config
    {
        #region Scopes
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
        {
            new ApiScope("Workout.Write","Write for Workout"),
            new ApiScope("Workout.Read","Read for Workout"),

            new ApiScope("Entertainment.Write","Write for Entertainment"),
            new ApiScope("Entertainment.Read","Read for Entertainment"),

            new ApiScope("Skill.Write","Write for Skill"),
            new ApiScope("Skill.Read","Read for Skill"),

            new ApiScope("Dashboard.Read","Read for Dashboard"),

            new ApiScope("Meal.Write","Write for Meal"),
            new ApiScope("Meal.Read","Read for Meal")
        };
        }
        #endregion

        #region Resources
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
        {
            new ApiResource("Workout"){
                ApiSecrets = {new Secret("workoutsecret".Sha256()) },
                Scopes = { "Workout.Write", "Workout.Read" }
            },
            new ApiResource("Entertainment"){
                ApiSecrets = {new Secret("entertainmentsecret".Sha256()) },
                Scopes = { "Entertainment.Write", "Entertainment.Read" }
            },
            new ApiResource("Skill"){
                ApiSecrets = {new Secret("skillsecret".Sha256()) },
                Scopes = { "Skill.Write", "Skill.Read" }
            },
            new ApiResource("Dashboard"){
                ApiSecrets = {new Secret("dashboardsecret".Sha256()) },
                Scopes = { "Dashboard.Read" }
            },
            new ApiResource("Meal"){
                ApiSecrets = {new Secret("mealsecret".Sha256()) },
                Scopes = { "Meal.Write", "Meal.Read" }
            }
        };
        }
        #endregion

        #region Clients
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client
                    {
                        ClientId = "Workout",
                        ClientName = "Workout",
                        ClientSecrets = { new Secret("workoutsecret".Sha256()) },
                        AllowedGrantTypes = { GrantType.ClientCredentials },
                        AllowedScopes = { "Workout.Write", "Workout.Read" }
                    },
            new Client
                    {
                        ClientId = "Entertainment",
                        ClientName = "Entertainment",
                        ClientSecrets = { new Secret("entertainmentsecret".Sha256()) },
                        AllowedGrantTypes = { GrantType.ClientCredentials },
                        AllowedScopes = { "Entertainment.Write", "Entertainment.Read" }
                    },
            new Client
                    {
                        ClientId = "Skill",
                        ClientName = "Skill",
                        ClientSecrets = { new Secret("skillsecret".Sha256()) },
                        AllowedGrantTypes = { GrantType.ClientCredentials },
                        AllowedScopes = { "Skill.Write", "Skill.Read" }
                    },
            new Client
                    {
                        ClientId = "Dashboard",
                        ClientName = "Dashboard",
                        ClientSecrets = { new Secret("dashboardsecret".Sha256()) },
                        AllowedGrantTypes = { GrantType.ClientCredentials },
                        AllowedScopes = { "Dashboard.Read" }
                    },
            new Client
                    {
                        ClientId = "Meal",
                        ClientName = "Meal",
                        ClientSecrets = { new Secret("mealsecret".Sha256()) },
                        AllowedGrantTypes = { GrantType.ClientCredentials },
                        AllowedScopes = { "Meal.Write", "Meal.Read" }
                    },
            new Client
                {
                    ClientId = "MeminToDoHome",
                    ClientName = "MeminToDoHome",
                    ClientSecrets = { new Secret("memintodohome".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile },
                    RedirectUris = { "https://localhost:7196/signin-oidc" },
                    RequirePkce = false
                }
        };
        }
        #endregion

        #region TestUsers
        public static IEnumerable<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
        new TestUser {
            SubjectId = "test-user1",
            Username = "test-user1",
            Password = "12345",
            Claims = {
                new Claim("name","test user1"),
                new Claim("website","https://wwww.testuser1.com"),
                new Claim("gender","1")
            }
        },
        new TestUser {
            SubjectId = "test-user2",
            Username = "test-user2",
            Password = "12345",
            Claims = {
                new Claim("name","test user2"),
                new Claim("website","https://wwww.testuser2.com"),
                new Claim("gender","0")
            }
        }
    };
        }
        #endregion

        #region IdentityResources
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        #endregion
    }
}
