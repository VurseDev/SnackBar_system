using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        
        app.MapPut("/produtos/reservar",
        async (SnackBarContext context, ReservaRequest req) =>
        {
            var product = await context.Produtos.FirstOrDefaultAsync(x => x.Id == req.produtoId);
            if (product is null)
                return Results.NotFound("Produto não encontrado");
            
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == req.usuarioId);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrado");

            product.ReservarProduto(req.quantidade);

            var mov = new Movimentacao(
                req.produtoId,
                req.usuarioId,
                req.tipo,
                req.quantidade
            );

            return Results.Ok(mov);
        });

        app.MapDelete("/produtos/{nome}",
        async(string nome, SnackBarContext context) =>
        {
            var product = await context.Produtos.FirstOrDefaultAsync(x => x.Nome == nome);

            if (product == null)
                return Results.NotFound();
            
            context.Remove(product);
            await context.SaveChangesAsync();

            return Results.Ok();
        });
    }
}