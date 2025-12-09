namespace SnackBar_system.Models;

public record UsuarioRequest(
    string nome,
    string email,
    string senha,
    string tipo
);
