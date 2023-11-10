using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(x =>
    {
        x.DefaultScheme = "MeminToDoHomeCookie";
        x.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("MeminToDoHomeCookie")
    .AddOpenIdConnect("oidc", x =>
    {
        x.SignInScheme = "MeminToDoHomeCookie";
        x.Authority = "https://localhost:8005";
        x.ClientId = "MeminToDoHome";
        x.ClientSecret = "memintodohome";
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


builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
