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
var app = builder.Build();

// Dados em memória (para simular um banco de dados)
var dados = new List<string> { "Item1", "Item2", "Item3" };

// GET: Retorna todos os itens
app.MapGet("/itens", () => dados);

// POST: Adiciona um novo item
app.MapPost("/itens", (string novoItem) =>
{
    dados.Add(novoItem);
    return Results.Created("/itens", novoItem);
});

// PUT: Atualiza um item existente pelo índice
app.MapPut("/itens/{indice}", (int indice, string itemAtualizado) =>
{
    if (indice < 0 || indice >= dados.Count)
    {
        return Results.NotFound("Índice inválido.");
    }

    dados[indice] = itemAtualizado;
    return Results.Ok(itemAtualizado);
});

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
});

// Inicia o servidor
app.Run();
