using QueseriaSoftware.DTOs.Resultados.Direccion;

namespace QueseriaSoftware.Services
{
    public interface IDireccionService
    {
        /// <summary>
        /// Crea una nueva dirección para un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario al que pertenece la dirección.</param>
        /// <param name="calle">Nombre de la calle.</param>
        /// <param name="numero">Número de la calle (opcional).</param>
        /// <param name="telefonoContacto">Teléfono de contacto asociado a la dirección.</param>
        /// <param name="idLocalidad">Identificador de la localidad (opcional).</param>
        /// <returns>Resultado de la creación de la dirección, que incluye información sobre éxito o error.</returns>
        Task<ResultadoCrearDireccion> CrearDireccion(int userId, string calle, int? numero, string telefonoContacto, int? idLocalidad);
    }

}
