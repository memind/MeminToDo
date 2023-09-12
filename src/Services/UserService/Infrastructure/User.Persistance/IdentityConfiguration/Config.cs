using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            new ApiScope("Dashboard.Read","Read for Dashboard")
        };
        }
        #endregion
        #region Resources
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
        {
            new ApiResource("Workout"){ Scopes = { "Workout.Write", "Workout.Read" } },
            new ApiResource("Entertainment"){ Scopes = { "Entertainment.Write", "Entertainment.Read" } },
            new ApiResource("Skill"){ Scopes = { "Skill.Write", "Skill.Read" } },
            new ApiResource("Dashboard"){ Scopes = { "Dashboard.Read" } }
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
                    }
        };
        }
        #endregion
    }
}
