using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Crie uma rota GET que retorna uma mensagem
app.MapGet("/mensagem", () => "Olá, essa é minha Web API!");

// Inicie o aplicativo
app.Run();
