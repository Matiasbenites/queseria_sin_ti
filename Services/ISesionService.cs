using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    public interface ISesionService
    {
        Task<UsuarioLoginDto?> AutenticarAsync(string nombreUsuario, string password);
        Task IniciarSesionAsync(UsuarioLoginDto usuario);
        Task CerrarSesionAsync();
        string CalcularHash(string input);

    }
}
