using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Services
{
    public class ProductosService : IProductosService
    {
        private readonly AppDbContext _context;

        public ProductosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ConsultarDisponibilidad(int productoId, int cantidad)
        {
            if (cantidad <= 0 || productoId <= 0)
            {
                return false;
            }

            var producto = await _context.Productos.FindAsync(productoId);

            if (producto != null && producto.Stock > cantidad)
            {
                return true;
            }

            return false;
        }

        public async Task<List<ProductoViewModel>> ConsultarCatalogo(string usuarioId)
        {

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

            return productos;
        }

        public void ActualizarProductosConEstadoDeCarrito(List<ProductoViewModel> productos, Dictionary<int, ProductoEnCarritoDto> productosEnCarrito)
        {
            foreach (var producto in productos)
            {
                if (productosEnCarrito.TryGetValue(producto.Id, out var linea))
                {
                    producto.CantidadEnCarrito = linea.Cantidad;
                    producto.CarritoLineaId = linea.CarritoLineaId;
                }
            }
        }
    }
}
