using QueseriaSoftware.DTOs.Resultados.Direccion;

namespace QueseriaSoftware.Services
{
    public interface IDireccionService
    {
        Task<ResultadoCrearDireccion> CrearDireccion(string calle, int? numero, string telefonoContacto, int? idLocalidad);
    }
}
