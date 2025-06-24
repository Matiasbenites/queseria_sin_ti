using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    /// <summary>
    /// Define las operaciones relacionadas con la gestión y consulta de productos.
    /// </summary>
    public interface IProductosService
    {
        /// <summary>
        /// Consulta el catálogo de productos disponibles para un usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario que realiza la consulta.</param>
        /// <returns>Una lista de <see cref="ProductoViewModel"/> con los productos disponibles.</returns>
        Task<List<ProductoViewModel>> ConsultarCatalogo(string usuarioId);

        /// <summary>
        /// Verifica si un producto tiene suficiente stock disponible para una cantidad solicitada.
        /// </summary>
        /// <param name="productoId">ID del producto a consultar.</param>
        /// <param name="cantidad">Cantidad requerida.</param>
        /// <returns><c>true</c> si hay stock suficiente; de lo contrario, <c>false</c>.</returns>
        Task<bool> ConsultarDisponibilidad(int productoId, int cantidad);

        /// <summary>
        /// Actualiza el estado visual de los productos según los productos agregados al carrito.
        /// </summary>
        /// <param name="productos">Lista de productos a actualizar.</param>
        /// <param name="productosEnCarrito">Diccionario con los productos actualmente en el carrito.</param>
        void ActualizarProductosConEstadoDeCarrito(
            List<ProductoViewModel> productos,
            Dictionary<int, ProductoEnCarritoDto> productosEnCarrito);

        /// <summary>
        /// Descuenta el stock de los productos incluidos en un pedido.
        /// </summary>
        /// <param name="productosPedido">Colección de productos con sus cantidades a descontar.</param>
        /// <returns>Un objeto <see cref="Resultado"/> que indica si la operación fue exitosa o no.</returns>
        Task<Resultado> DescontarStock(ICollection<PedidoDetalle>? productosPedido);
    }
}
