using Microsoft.EntityFrameworkCore;
using SnackBar_system.Models;

namespace SnackBar_system.Data;

public class SnackBarContext : DbContext
{
    public SnackBarContext(DbContextOptions<SnackBarContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Movimentacao> Movimentacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Movimentacao>()
            .HasOne<Produto>()                       
            .WithMany()                             
            .HasForeignKey(m => m.ProdutoId)        
            .OnDelete(DeleteBehavior.Restrict);      

        modelBuilder.Entity<Movimentacao>()
            .HasOne<Usuario>()                       
            .WithMany()
            .HasForeignKey(m => m.UsuarioId)         
            .OnDelete(DeleteBehavior.Restrict);      

        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Produto>().ToTable("Produtos");
        modelBuilder.Entity<Movimentacao>().ToTable("Movimentacoes");
    }
}
