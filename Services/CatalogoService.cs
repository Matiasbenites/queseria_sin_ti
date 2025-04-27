using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly AppDbContext _context;
        private readonly ICarritoService _carritoService;

        public CatalogoService(AppDbContext context, ICarritoService carritoService)
        {
            _context = context;
            _carritoService = carritoService;
        }

        public async Task<int> ConsultarDisponibilidad(int productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            return producto?.Stock ?? 0;
        }

        public async Task<List<ProductoViewModel>> ConsultarCatalogo(string busqueda)
        {
            var carrito = await _context.CarritoLineas
                .Where(cl => cl.Carrito.IdUsuario == 1)
                .ToListAsync();

            var query = _context.Productos
                .Where(p => p.Activo && p.Stock > 0);

            if (!string.IsNullOrEmpty(busqueda))
            {
                query = query.Where(p => p.Nombre == busqueda);
            }
            else
            {
                query = query.Take(5);
            }

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

            // 🚀 Acá, ya en memoria, agregás la cantidad de carrito
            foreach (var producto in productos)
            {
                producto.CantidadEnCarrito = carrito
                    .FirstOrDefault(c => c.ProductoId == producto.Id)?.Cantidad;
            }

            return productos;
        }
    }
}
