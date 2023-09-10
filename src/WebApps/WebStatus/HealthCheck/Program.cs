using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Consul;
using HealthCheck.Consul;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

#region Consul
//builder.Services.AddSingleton<IConsulClient>(consul => new ConsulClient(cfg =>
//{
//    cfg.Address = new Uri(builder.Configuration["Consul:Host"]);
//}, null, handlerOverride =>
//{
//    handlerOverride.Proxy = null;
//    handlerOverride.UseProxy = false;
//}));

//builder.Services.Configure<WorkoutConfiguration>(builder.Configuration.GetSection("WorkoutService"));
//builder.Services.Configure<EntertainmentConfiguration>(builder.Configuration.GetSection("EntertainmentService"));
//builder.Services.Configure<SkillConfiguration>(builder.Configuration.GetSection("SkillService"));
//builder.Services.Configure<DashboardConfiguration>(builder.Configuration.GetSection("DashboardAggregator"));
//builder.Services.AddSingleton<IHostedService, ConsulRegisterServices>();
#endregion

#region Appmetrics - Prometheus - Grafana
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

builder.Services.AddMetrics();

builder.Host.UseMetricsWebTracking()
                .UseMetrics(options =>
                {
                    options.EndpointOptions = endpointsOptions =>
                    {
                        endpointsOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                        endpointsOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
                        endpointsOptions.EnvironmentInfoEndpointEnabled = false;
                    };
                });

builder.Services.AddMvcCore().AddMetricsCore();
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
app.UseHttpMetrics();
app.MapMetrics();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecksUI();
app.Run();