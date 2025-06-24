using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    /// <summary>
    /// Define las operaciones relacionadas con los usuarios, particularmente sus direcciones.
    /// </summary>
    public interface IUsuariosService
    {
        /// <summary>
        /// Agrega una nueva dirección a un usuario dado.
        /// </summary>
        /// <param name="usuarioId">ID del usuario al que se le agregará la dirección.</param>
        /// <param name="calle">Nombre de la calle.</param>
        /// <param name="numero">Número de la calle (puede ser nulo).</param>
        /// <param name="telefonoContacto">Teléfono de contacto para la dirección.</param>
        /// <param name="idLocalidad">ID de la localidad asociada a la dirección.</param>
        /// <returns>Un resultado indicando si la operación fue exitosa o si hubo errores.</returns>
        Task<ResultadoAgregarDireccionAUsuario> AgregarDireccionUsuario(
            string usuarioId,
            string calle,
            int? numero,
            string telefonoContacto,
            int? idLocalidad);

        /// <summary>
        /// Obtiene la lista de direcciones asociadas a un usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>Una lista de direcciones en formato ViewModel.</returns>
        Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId);

        /// <summary>
        /// Valida si una dirección pertenece al usuario actual y está activa.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="direccionId">ID de la dirección a validar.</param>
        /// <returns>Un objeto Resultado indicando si la dirección es válida o no.</returns>
        Task<Resultado> ValidarDireccion(string usuarioId, int direccionId);
    }
}
