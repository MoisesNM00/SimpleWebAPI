/*

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Crie uma rota GET que retorna uma mensagem
app.MapGet("/mensagem", () => "Olá, essa é minha Web API!");

// Inicie o aplicativo
app.Run();

*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Dados em memória
var dados = new List<string> { "Item1", "Item2", "Item3" };

// GET: Retorna todos os itens
app.MapGet("/itens", () => dados)
    .WithName("GetItens") // Nome para a operação no Swagger
    .WithOpenApi();       // Habilita documentação específica para essa rota

// POST: Adiciona um novo item
app.MapPost("/itens", (string novoItem) =>
{
    dados.Add(novoItem);
    return Results.Created("/itens", novoItem);
})
    .WithName("AddItem")
    .WithOpenApi();

// PUT: Atualiza um item pelo índice
app.MapPut("/itens/{indice}", (int indice, string itemAtualizado) =>
{
    if (indice < 0 || indice >= dados.Count)
    {
        return Results.NotFound("Índice inválido.");
    }

    dados[indice] = itemAtualizado;
    return Results.Ok(itemAtualizado);
})
    .WithName("UpdateItem")
    .WithOpenApi();

// DELETE: Remove um item pelo índice
app.MapDelete("/itens/{indice}", (int indice) =>
{
    if (indice < 0 || indice >= dados.Count)
    {
        return Results.NotFound("Índice inválido.");
    }

    var itemRemovido = dados[indice];
    dados.RemoveAt(indice);
    return Results.Ok($"Item removido: {itemRemovido}");
})
    .WithName("DeleteItem")
    .WithOpenApi();

// Inicia o servidor
app.Run();

// Use o link http://localhost:5000/swagger para acessar a documentação da API
