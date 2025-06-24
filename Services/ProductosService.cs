using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Sp;
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
            var resultado = await _context
                .Set<DisponibleResultado>() // DTO temporal
                .FromSqlRaw("EXEC ConsultarDisponibilidad @p0, @p1", productoId, cantidad)
                .ToListAsync();

            return resultado.FirstOrDefault()?.Disponible == 1;
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

        public async Task<Resultado> DescontarStock(ICollection<PedidoDetalle>? productosPedido)
        {
            var resultado = new Resultado();

            if (productosPedido == null || !productosPedido.Any())
            {
                resultado.Success = false;
                resultado.Message = "No se encontraron productos en el pedido.";
                return resultado;
            }

            foreach (var item in productosPedido)
            {
                var producto = await _context.Productos.FindAsync(item.IdProducto);

                if (producto == null)
                {
                    resultado.Success = false;
                    resultado.Message = $"Producto con ID {item.IdProducto} no encontrado.";
                    return resultado;
                }

                if (producto.Stock < item.Cantidad)
                {
                    resultado.Success = false;
                    resultado.Message = $"No hay suficiente stock.";
                    return resultado;
                }

                producto.Stock -= item.Cantidad;
                _context.Productos.Update(producto);
            }

            await _context.SaveChangesAsync();

            resultado.Success = true;
            resultado.Message = "Stock descontado correctamente.";
            return resultado;
        }

    }
}
