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

        public ProductosService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> ConsultarDisponibilidad(int productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            return producto?.Stock ?? 0;
        }

        public async Task<List<ProductoViewModel>> ConsultarCatalogo()
        {
            int userId = GetCurrentUserId();

            var carrito = await _context.CarritoLineas
                .Where(cl => cl.Carrito.IdUsuario == userId)
                .ToListAsync();

            var query = _context.Productos
                .Where(p => p.Activo && p.Stock > 0);

            var productos = await query
                .Select(p => new ProductoViewModel
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion,
                    Id = p.Id,
                    ImagenUrl = p.ImgUrl != null ? p.ImgUrl : "/imagenes/sin-imagen.jpg",
                    Stock = p.Stock
                })
                .ToListAsync();

            foreach (var producto in productos)
            {
                producto.CantidadEnCarrito = carrito
                    .FirstOrDefault(c => c.ProductoId == producto.Id)?
                    .Cantidad;
                if (producto.CantidadEnCarrito != null)
                {
                    producto.CarritoLineaId = carrito
                        .FirstOrDefault(c => c.ProductoId == producto.Id)
                        .Id;
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
