namespace SnackBar_system.Models;

public class Movimentacao
{
    public Movimentacao(Guid produtoId, Guid usuarioId, string tipo, int quantidade, string fornecedor)
    {
        Id = Guid.NewGuid();
        ProdutoId = produtoId;
        UsuarioId = usuarioId;
        Tipo = tipo;
        Quantidade = quantidade;
        Fornecedor = fornecedor;
    }

    public Guid Id { get; init; }
    public Guid ProdutoId { get; set; }
    public Guid UsuarioId { get; set; }
    public string Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataHora { get; set; } = DateTime.Now;
    public string Fornecedor { get; set; }
}