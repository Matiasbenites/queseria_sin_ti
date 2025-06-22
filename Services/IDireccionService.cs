using QueseriaSoftware.DTOs.Resultados.Direccion;

namespace QueseriaSoftware.Services
{
    public interface IDireccionService
    {
        Task<ResultadoCrearDireccion> CrearDireccion(int userId, string calle, int? numero, string telefonoContacto, int? idLocalidad);
    }
}
