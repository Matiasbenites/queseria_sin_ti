using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface ICarritoService
    {
        /// <summary>
        /// Agrega un producto al carrito del usuario con una cantidad determinada.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="productoId">Identificador del producto a agregar.</param>
        /// <param name="cantidad">Cantidad del producto a agregar.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        Task<Resultado> AgregarProducto(string usuarioId, int productoId, int cantidad);

        /// <summary>
        /// Agrega un producto a una línea específica del carrito proporcionado.
        /// </summary>
        /// <param name="carrito">Carrito donde se agregará el producto (puede ser null).</param>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="productoId">Identificador del producto a agregar.</param>
        /// <param name="cantidad">Cantidad del producto a agregar.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        Task<Resultado> AgregarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad);

        /// <summary>
        /// Obtiene el carrito de compras asociado a un usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>El carrito si existe, o null si no se encontró.</returns>
        Task<Carrito?> ObtenerCarrito(string usuarioId);

        /// <summary>
        /// Obtiene una vista modelo detallada del carrito de un usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>El carrito del usuario en forma de ViewModel.</returns>
        Task<CarritoViewModel> ObtenerCarritoUsuario(string usuarioId);

        /// <summary>
        /// Modifica la cantidad de un producto específico dentro del carrito del usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="productoId">Identificador del producto a modificar.</param>
        /// <param name="cantidad">Nueva cantidad del producto.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        Task<Resultado> ModificarCantidadProducto(string usuarioId, int productoId, int cantidad);

        /// <summary>
        /// Elimina un producto del carrito del usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <param name="productoId">Identificador del producto a eliminar.</param>
        Task EliminarProductoCarrito(string usuarioId, int productoId);

        /// <summary>
        /// Obtiene el total de ítems diferentes que hay en el carrito de un usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Número total de ítems en el carrito.</returns>
        Task<int> ObtenerTotalItems(string usuarioId);

        /// <summary>
        /// Obtiene una línea específica del carrito por su identificador.
        /// </summary>
        /// <param name="lineaId">Identificador de la línea del carrito.</param>
        /// <returns>La línea del carrito correspondiente.</returns>
        Task<CarritoLinea> ObtenerLineaCarrito(int lineaId);

        /// <summary>
        /// Obtiene un diccionario con los productos contenidos en el carrito de un usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Diccionario con la clave como ID del producto y el valor con detalles del producto en el carrito.</returns>
        Task<Dictionary<int, ProductoEnCarritoDto>> ObtenerProductosDelCarrito(int usuarioId);

        /// <summary>
        /// Elimina todo el carrito de un usuario.
        /// </summary>
        /// <param name="usuarioId">Identificador del usuario.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        Task<Resultado> Eliminar(string usuarioId);
    }

}
