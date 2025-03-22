using OrderManagementAPI.Data;
using OrderManagementAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuração do RabbitMQ
builder.Services.AddSingleton<RabbitMQService>(sp =>
{
    var connectionString = builder.Configuration["RabbitMQ:ConnectionString"];
    return new RabbitMQService(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do MongoDB
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Adiciona o serviço do MongoDB
builder.Services.AddSingleton<MongoDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
