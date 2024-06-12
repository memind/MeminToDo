using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;
using Common.Logging;
using Common.Logging.Handlers;
using Dashboard.Aggregator.Extensions;
using Dashboard.Aggregator.Services.Abstractions;
using Dashboard.Aggregator.Services.Concretes;
using HealthChecks.UI.Client;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using Prometheus;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(opt => opt.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region IdentityServer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "http://localhost:8005";
        options.Audience = "Dashboard";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(authOption =>
{
    authOption.AddPolicy("DashboardRead", policy => policy.RequireClaim("scope", "Dashboard.Read"));
});
#endregion

#region SeriLog
builder.Host.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.Configure(options =>
    {
        options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId;
    });
}).UseSerilog(SeriLogger.Configure);

builder.Services.AddTransient<LoggingDelegatingHandler>();
#endregion

#region HealthCheck
builder.Services.AddHealthChecks()
                    .AddUrlGroup(new Uri($"{builder.Configuration["ApiSettings:WorkoutUrl"]}/swagger/index.html"), "Workout.API", HealthStatus.Degraded)
                    .AddUrlGroup(new Uri($"{builder.Configuration["ApiSettings:EntertainmentUrl"]}/swagger/index.html"), "Entertainment.API", HealthStatus.Degraded)
                    .AddUrlGroup(new Uri($"{builder.Configuration["ApiSettings:SkillUrl"]}/swagger/index.html"), "Skill.API", HealthStatus.Degraded);
#endregion

#region OpenTracing/Jaeger
builder.Services.AddSingleton<ITracer>(sp =>
{
    var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender("host.docker.internal", 6831, 0))
        .Build();
    var tracer = new Tracer.Builder(serviceName)
        .WithSampler(new ConstSampler(true))
        .WithReporter(reporter)
        .Build();

    if (!GlobalTracer.IsRegistered())
    {
        GlobalTracer.Register(tracer);
    }
    return tracer;
});

builder.Services.AddOpenTracing();

builder.Services.Configure<HttpHandlerDiagnosticOptions>(options =>
    options.OperationNameResolver =
        request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");

builder.Services.AddHttpClient();


builder.Services.Configure<AspNetCoreDiagnosticOptions>(options =>
{
    options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/status"));
    options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/metrics"));
});
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

builder.Services.AddTransient<IDashboardService, DashboardService>();

builder.Services.AddHttpClient<IWorkoutService, WorkoutService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:WorkoutUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
                .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IEntertainmentService, EntertainmentService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:EntertainmentUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
                .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<ISkillService, SkillService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:SkillUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
                .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IMealService, MealService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:MealUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
                .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IBudgetService, BudgetService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ApiSettings:BudgetUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PollyPolicies.GetRetryPolicy())
                .AddPolicyHandler(PollyPolicies.GetCircuitBreakerPolicy());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics();
app.MapControllers();

app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();