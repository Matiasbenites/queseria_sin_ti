using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IProductosService
    {
        Task<List<ProductoViewModel>> ConsultarCatalogo(string usuarioId);

        Task<bool> ConsultarDisponibilidad(int productoId, int cantidad);

        void ActualizarProductosConEstadoDeCarrito(List<ProductoViewModel> productos, Dictionary<int, ProductoEnCarritoDto> productosEnCarrito);

        Task<Resultado> DescontarStock(ICollection<PedidoDetalle>? productosPedido);

    }
}
