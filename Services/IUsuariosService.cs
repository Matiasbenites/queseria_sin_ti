using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    public interface IUsuariosService
    {
        Task<bool> ValidarUsuario(int usuarioId);

        string ObtenerUsuarioId();

        bool EsAutenticado();
    }
}
