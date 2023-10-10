using Meal.Infrastructure;
using Meal.Application;
using Meal.Mapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddInfrastructureServices(builder.Configuration, builder.Host);
builder.Services.AddApplicationServices();
builder.Services.AddCustomMapper();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseHttpLogging();
app.UseSerilogRequestLogging();
app.UseAuthorization();
app.MapControllers();
app.Run();