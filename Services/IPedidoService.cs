using QueseriaSoftware.DTOs.Resultados.Pedido;
using QueseriaSoftware.Models.EstadosPedido;

namespace QueseriaSoftware.Services
{
    public interface IPedidoService
    {

        Task<ResultadoCrearPedido> CrearPedido(string usuarioId, string estado);
        Task<string> ObtenerEstadoUltimoPedido(string idUsuario);
    }
}
