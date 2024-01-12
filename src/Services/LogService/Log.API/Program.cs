using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Hangfire;
using MassTransit;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;
using Common.Messaging.MassTransit.Services.ApiServices.Connected;
using Common.Messaging.MassTransit.Consts;
using Common.Messaging.MassTransit.Services.ApiServices.Test;
using Common.Messaging.MassTransit.Services.ApiServices.BackUp;
using Microsoft.Extensions.Configuration;
using Log.API;
using Common.Messaging.RabbitMQ.Configurations;

var builder = WebApplication.CreateBuilder(args);

#region AWS
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
#endregion

#region RabbitMQ
builder.Services.AddScoped<IMessagePublisherService, MessagePublisherService>();
builder.Services.Configure<RabbitMqUri>(builder.Configuration.GetSection("RabbitMqHost"));
#endregion

#region Hangfire
builder.Services.AddHangfireServer();
builder.Services.AddHangfire(configuration => configuration
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("MsSqlHangfire")));
#endregion

#region MassTransit
builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<ConsumeConnectedMessageService>();

    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration.GetSection("RabbitMqHost").Value);

        _configurator.ReceiveEndpoint(MessagingConsts.GetConnectedInfoQueue(), e => e.ConfigureConsumer<ConsumeConnectedMessageService>(context));
    });
});

builder.Services.AddHostedService<PublishTestMessageService>(provider =>
{
    using IServiceScope scope = provider.CreateScope();
    IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

    return new(publishEndpoint);
});

builder.Services.AddHostedService<PublishBackUpMessageService>(provider =>
{
    using IServiceScope scope = provider.CreateScope();
    IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();

    return new(publishEndpoint);
});
#endregion

builder.Services.AddMvc();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHangfireDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();