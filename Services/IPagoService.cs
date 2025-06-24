using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pagos;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    namespace QueseriaSoftware.Services
    {
        /// <summary>
        /// Define las operaciones relacionadas con los pagos y medios de pago.
        /// </summary>
        public interface IPagoService
        {
            /// <summary>
            /// Obtiene los datos necesarios para mostrar los medios de pago disponibles al usuario.
            /// </summary>
            /// <param name="usuarioId">ID del usuario que va a realizar el pago.</param>
            /// <returns>Un objeto <see cref="ResultadoDatosDePago"/> con la información de pago del usuario.</returns>
            public Task<ResultadoDatosDePago> ObtenerDatosDePago(string usuarioId);

            /// <summary>
            /// Obtiene la lista de medios de pago habilitados.
            /// </summary>
            /// <returns>Una lista de <see cref="PagosViewModel"/> que representan los medios de pago disponibles.</returns>
            Task<List<PagosViewModel>> ObtenerMediosPago();

            /// <summary>
            /// Procesa el pago de un pedido con el medio de pago seleccionado por el usuario.
            /// </summary>
            /// <param name="medioPagoId">ID del medio de pago seleccionado.</param>
            /// <param name="usuarioId">ID del usuario que realiza el pago.</param>
            /// <returns>Un objeto <see cref="ResultadoConfirmarPedido"/> con el resultado del procesamiento del pago.</returns>
            Task<ResultadoConfirmarPedido> ProcesarPago(int medioPagoId, string usuarioId);
        }
    }

}
