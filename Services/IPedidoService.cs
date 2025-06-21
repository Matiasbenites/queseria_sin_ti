using QueseriaSoftware.Models.EstadosPedido;

namespace QueseriaSoftware.Services
{
    public interface IPedidoService
    {
        Task<string> ObtenerEstadoUltimoPedido(string idUsuario);
    }
}
