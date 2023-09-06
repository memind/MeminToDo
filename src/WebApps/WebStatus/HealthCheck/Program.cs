using Consul;
using HealthCheck.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

#region Consul
builder.Services.AddSingleton<IConsulClient>(consul => new ConsulClient(cfg =>
{
    cfg.Address = new Uri(builder.Configuration["Consul:Host"]);
}, null, handlerOverride =>
{
    handlerOverride.Proxy = null;
    handlerOverride.UseProxy = false;
}));

builder.Services.Configure<WorkoutConfiguration>(builder.Configuration.GetSection("WorkoutService"));
builder.Services.Configure<EntertainmentConfiguration>(builder.Configuration.GetSection("EntertainmentService"));
builder.Services.Configure<SkillConfiguration>(builder.Configuration.GetSection("SkillService"));
builder.Services.AddSingleton<IHostedService, ConsulRegisterServices>();
#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecksUI();
app.Run();