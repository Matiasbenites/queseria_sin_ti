using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface ICarritoService
    {
        Task<Resultado> AgregarProductoCarrito(string usuarioId, int productoId, int cantidad);

        Task<Resultado> AgregarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad);

        Task<Carrito?> ObtenerCarrito(string usuarioId);

        Task<CarritoViewModel> ObtenerCarritoCompleto(string usuarioId);

        Task ActualizarCantidad(int lineaId, int cantidad);

        Task EliminarLinea(int lineaId);

        Task<int> ObtenerTotalItems(string usuarioId);

        Task<CarritoLinea> ObtenerLineaCarrito(int lineaId);

        Task<Dictionary<int, ProductoEnCarritoDto>> ObtenerProductosDelCarrito(int usuarioId);
    }
}
