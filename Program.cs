using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using DotnetBackend.Database;
using DotnetBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração para obter o DatabaseSettings a partir do appsettings.json
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

// Adiciona o serviço de MongoDB
builder.Services.AddSingleton<MongoDBService>();

// Registra o serviço de usuário
builder.Services.AddSingleton<UserService>();

// Adiciona os controladores
builder.Services.AddControllers();

// Configura o Swagger para documentação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware para Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();