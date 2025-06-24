using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pedido;
using QueseriaSoftware.Models;
using QueseriaSoftware.Models.EstadosPedido;

namespace QueseriaSoftware.Services
{
    /// <summary>
    /// Define las operaciones relacionadas con la gestión de pedidos.
    /// </summary>
    public interface IPedidoService
    {
        /// <summary>
        /// Crea un nuevo pedido para el usuario especificado.
        /// </summary>
        /// <param name="usuarioId">ID del usuario que realiza el pedido.</param>
        /// <param name="estado">Estado inicial del pedido (por ejemplo, "Nuevo").</param>
        /// <returns>Un objeto <see cref="ResultadoCrearPedido"/> que contiene el resultado de la operación.</returns>
        Task<ResultadoCrearPedido> CrearPedido(string usuarioId, string estado);

        /// <summary>
        /// Obtiene el estado actual del último pedido realizado por el usuario.
        /// </summary>
        /// <param name="idUsuario">ID del usuario.</param>
        /// <returns>Una cadena con el estado del pedido (por ejemplo, "En preparación", "Entregado", etc.).</returns>
        Task<string> ObtenerEstadoUltimoPedido(string idUsuario);

        /// <summary>
        /// Obtiene el último pedido realizado por el usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>El objeto <see cref="Pedido"/> correspondiente al último pedido, o <c>null</c> si no existe.</returns>
        Task<Pedido?> ObtenerUltimoPedido(string usuarioId);

        /// <summary>
        /// Asocia una dirección existente al último pedido del usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="direccionId">ID de la dirección que se desea asociar.</param>
        /// <returns>Un objeto <see cref="Resultado"/> indicando si la operación fue exitosa.</returns>
        Task<Resultado> AgregarDireccionAlPedido(string usuarioId, int direccionId);

        /// <summary>
        /// Cambia el estado del pedido al siguiente estado definido por la lógica del patrón State.
        /// </summary>
        /// <param name="pedido">El pedido que se desea actualizar.</param>
        void CambiarASiguienteEstado(Pedido pedido);

        /// <summary>
        /// Calcula el total del pedido del usuario, sumando los subtotales de todos los productos.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>El total del pedido como decimal.</returns>
        Task<decimal> CalcularTotalPedido(string usuarioId);
    }

}
