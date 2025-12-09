using Microsoft.EntityFrameworkCore;
using SnackBar_system.Data;

namespace SnackBar_system.Models;

public static class UsuarioRoute
{
    public static void UsuarioRoutes(this WebApplication app)
    {
        app.MapGet("/usuarios", 
        async (SnackBarContext context) =>
        {
            var user = await context.Usuarios.ToListAsync();
            return Results.Ok(user);
        });

        app.MapPost("/usuarios",
        async (SnackBarContext context, UsuarioRequest req) =>
        {
            var user = new Usuario(req.nome, req.email, req.senha, req.tipo);

            await context.Usuarios.AddAsync(user);
            await context.SaveChangesAsync();
            return Results.Ok(user);
        });

        

    }
}