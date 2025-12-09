using Microsoft.EntityFrameworkCore;
using SnackBar_system.Data;
using SnackBar_system.Models;

namespace SnackBar_system;

public static class ProdutoRoute
{
    public static void ProdutosRoutes(this WebApplication app)
    {
        var route = app.MapGroup("produtos");

        app.MapGet("/produtos",
        async(SnackBarContext context) =>
        {
            var product = await context.Produtos.ToListAsync();
            return Results.Ok(product);
        });

        app.MapPost("/produtos",
        async(SnackBarContext context, ProdutoRequest req) =>
        {
            var product = new Produto(
                req.nome,
                req.categoria,
                req.preco,
                req.quantidadeTotal,
                req.quantidadeDisponivel,
                req.quantidadeReserva
            );

            context.Produtos.Add(product);
            await context.SaveChangesAsync();
        });
        
    }
}