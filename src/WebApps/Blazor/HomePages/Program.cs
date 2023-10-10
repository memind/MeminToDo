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
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
