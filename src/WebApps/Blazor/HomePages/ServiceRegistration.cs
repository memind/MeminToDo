using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace HomePages
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(x =>
            {
                x.DefaultScheme = "MeminToDoHomeCookie";
                x.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("MeminToDoHomeCookie")
                .AddOpenIdConnect("oidc", x =>
                {
                    x.SignInScheme = "MeminToDoHomeCookie";
                    x.Authority = "https://localhost:8005";
                    x.ClientId = cfg.GetValue<string>("Client:Id");
                    x.ClientSecret = cfg.GetValue<string>("Client:Secret");
                    x.ResponseType = "code id_token";
                    x.GetClaimsFromUserInfoEndpoint = true;
                    x.Scope.Add("offline_access");
                    x.SaveTokens = true;

                    x.Scope.Add("Industry");
                    x.Scope.Add("Wage");
                    x.Scope.Add("PositionAndAuthority");
                    x.Scope.Add("WorkingAt");
                    x.Scope.Add("Roles");

                    x.ClaimActions.MapUniqueJsonKey("industry", "industry");
                    x.ClaimActions.MapUniqueJsonKey("wage", "wage");
                    x.ClaimActions.MapUniqueJsonKey("position", "position");
                    x.ClaimActions.MapUniqueJsonKey("authority", "authority");
                    x.ClaimActions.MapUniqueJsonKey("workingat", "workingat");
                    x.ClaimActions.MapUniqueJsonKey("role", "role");

                    x.TokenValidationParameters = new TokenValidationParameters { RoleClaimType = "role" };
                });


            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            return services;
        }
    }
}
