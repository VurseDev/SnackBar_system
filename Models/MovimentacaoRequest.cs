namespace SnackBar_system.Models;

public record EntradaRequest(
    Guid produtoId,
    Guid usuarioId,
    int quantidade,
    string tipo = "Entrada"
);
public record SaidaRequest(
    Guid produtoId,
    Guid usuarioId,
    int quantidade,
    string tipo = "Saida"
);