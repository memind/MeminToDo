using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Hangfire;
using Common.Messaging.RabbitMQ.Abstract;
using Common.Messaging.RabbitMQ.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

builder.Services.AddScoped<IMessagePublisherService, MessagePublisherService>();

builder.Services.AddHangfireServer();
builder.Services.AddHangfire(configuration => configuration
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("MsSqlHangfire")));

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