using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Services
{
    public class ProductosService : IProductosService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICarritoService _carritoService;

        public ProductosService(AppDbContext context, IHttpContextAccessor httpContextAccessor, ICarritoService carritoService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _carritoService = carritoService;
        }

        public async Task<bool> ConsultarDisponibilidad(int productoId, int cantidad)
        {
            var producto = await _context.Productos.FindAsync(productoId);

            if (producto != null && producto.Stock > cantidad)
            {
                return true;
            }

            return false;
        }

        public async Task<List<ProductoViewModel>> ConsultarCatalogo()
        {
            int userId = GetCurrentUserId();

            // Traer productos activos con stock
            var productos = await _context.Productos
                .Where(p => p.Activo && p.Stock > 0)
                .Select(p => new ProductoViewModel
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion,
                    Id = p.Id,
                    ImagenUrl = p.ImgUrl ?? "/imagenes/sin-imagen.jpg",
                    Stock = p.Stock
                })
                .ToListAsync();

            // Obtener productos en el carrito del usuario como diccionario
            var productosEnCarrito = await _carritoService.ObtenerProductosDelCarrito(userId);

            // Enlazar cada producto del catálogo con la información del carrito (si corresponde)
            foreach (var producto in productos)
            {
                if (productosEnCarrito.TryGetValue(producto.Id, out var linea))
                {
                    producto.CantidadEnCarrito = linea.Cantidad;
                    producto.CarritoLineaId = linea.CarritoLineaId;
                }
            }

            return productos;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new Exception("No se encontró el ID del usuario.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
