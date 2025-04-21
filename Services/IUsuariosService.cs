using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    public interface IUsuariosService
    {
        Usuario? Autenticar(string nombreUsuario, string password);

    }
}
