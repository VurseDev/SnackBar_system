using Microsoft.EntityFrameworkCore;
using SnackBar_system.Data;
using SnackBar_system.Models;

namespace SnackBar_system;

public static class MovimentacaoRoute
{
    public static void MovimentacoesRoute(this WebApplication app)
    {
        var route = app.MapGroup("movimentacoes");

        //registrar entrada
        app.MapPost("/produtos",
        async (SnackBarContext context, EntradaRequest req) =>
        {
            var product = await context.Produtos.FirstOrDefaultAsync(x => x.Id == req.produtoId);
            if (product is null)
                return Results.NotFound("Produto não encontrado");
            
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == req.usuarioId);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrado");

            product.AdicionarEstoque(req.quantidade);

            var mov = new Movimentacao(
                req.produtoId,
                req.usuarioId,
                req.tipo,
                req.quantidade
            );

            return Results.Ok(mov);
        });

        //registrar saida
        app.MapPost("/produtos",
        async (SnackBarContext context, SaidaRequest req) =>
        {
            var product = await context.Produtos.FirstOrDefaultAsync(x => x.Id == req.produtoId);
            if (product is null)
                return Results.NotFound("Produto não encontrado");
            
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == req.usuarioId);
            if (usuario is null)
                return Results.NotFound("Usuário não encontrado");

            product.RegistrarVenda(req.quantidade);

            var mov = new Movimentacao(
                req.produtoId,
                req.usuarioId,
                req.tipo,
                req.quantidade
            );

            return Results.Ok(mov);
        });
    }
}