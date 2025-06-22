using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pedido;
using QueseriaSoftware.Models;
using QueseriaSoftware.Models.EstadosPedido;

namespace QueseriaSoftware.Services
{
    public interface IPedidoService
    {

        Task<ResultadoCrearPedido> CrearPedido(string usuarioId, string estado);
        Task<string> ObtenerEstadoUltimoPedido(string idUsuario);

        Task<Pedido?> ObtenerUltimoPedido(string usuarioId);

        Task<Resultado> AgregarDireccionAlPedido(string usuarioId, int direccionId);
    }
}
