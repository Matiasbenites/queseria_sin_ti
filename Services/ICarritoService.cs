using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface ICarritoService
    {
        Task<Resultado> AgregarProducto(string usuarioId, int productoId, int cantidad);

        Task<Resultado> AgregarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad);

        Task<Carrito?> ObtenerCarrito(string usuarioId);

        Task<CarritoViewModel> ObtenerCarritoUsuario(string usuarioId);

        Task<Resultado> ModificarCantidadProducto(string usuarioId, int productoId, int cantidad);

        Task EliminarProductoCarrito(string usuarioId, int productoId);

        Task<int> ObtenerTotalItems(string usuarioId);

        Task<CarritoLinea> ObtenerLineaCarrito(int lineaId);

        Task<Dictionary<int, ProductoEnCarritoDto>> ObtenerProductosDelCarrito(int usuarioId);
    }
}
