using Autofac.Extensions.DependencyInjection;
using Autofac;
using Entertainment.Application;
using Entertainment.Persistance;
using Entertainment.Persistance.DependencyResolver.Autofac;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices(builder.Configuration, builder.Host);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacDependencyResolver());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCors();
app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();