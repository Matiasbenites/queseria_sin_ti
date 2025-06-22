using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IUsuariosService
    {
        Task<ResultadoAgregarDireccionAUsuario> AgregarDireccionUsuario(string usuarioId, string calle, int? numero, string telefonoContacto, int? idLocalidad);

        Task<Resultado> AsociarDireccionUsuario(string usuarioId, int? direccionId);

        Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId);
        Task<bool> ValidarUsuario(int usuarioId);

        string ObtenerUsuarioId();

        bool EsAutenticado();
    }
}
