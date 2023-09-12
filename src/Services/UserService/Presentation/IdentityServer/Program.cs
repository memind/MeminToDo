using User.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();
app.Run();