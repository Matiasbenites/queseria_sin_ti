using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IUsuariosService
    {
        Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId);
        Task<bool> ValidarUsuario(int usuarioId);

        string ObtenerUsuarioId();

        bool EsAutenticado();
    }
}
