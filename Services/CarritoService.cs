using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly AppDbContext _context;
        private readonly IProductosService _productosService;

        public CarritoService(AppDbContext context, IProductosService productosService)
        {
            _context = context;
            _productosService = productosService;
        }
        public async Task<Resultado> AgregarProducto(string usuarioId, int productoId, int cantidad)
        {
            var resultado = new Resultado();

            var validacionCarrito = await ValidarUsuarioCarritoStock(usuarioId, productoId, cantidad);

            if (!validacionCarrito.Success)
            {
                resultado.Success = validacionCarrito.Success;
                resultado.Message = validacionCarrito.Message;
                return resultado;
            }

            resultado = await AgregarProductoCarritoLinea(validacionCarrito.Carrito, usuarioId, productoId, cantidad);

            return resultado;
        }

        public async Task<Resultado> ModificarCantidadProducto(string usuarioId, int productoId, int cantidad)
        {
            var resultado = new Resultado();
            var validacionCarrito = await ValidarUsuarioCarritoStock(usuarioId, productoId, cantidad);

            if (!validacionCarrito.Success)
            {
                resultado.Success = validacionCarrito.Success;
                resultado.Message = validacionCarrito.Message;
                return resultado;
            }

            resultado = await ActualizarProductoCarritoLinea(validacionCarrito.Carrito, usuarioId, productoId, cantidad);
            return resultado;
        }

        public async Task<Resultado> ActualizarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad)
        {
            try
            {
                if (carrito == null)
                {
                    return new Resultado
                    {
                        Success = false,
                        Message = "Carrito no encontrado para el usuario"
                    };
                }

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC ActualizarProductoCarritoLinea @p0, @p1, @p2",
                    carrito.Id, productoId, cantidad
                );

                return new Resultado
                {
                    Success = true,
                    Message = "Carrito actualizado"
                };
            }
            catch (Exception ex)
            {
                return new Resultado
                {
                    Success = false,
                    Message = "Error al actualizar el carrito: " + ex.Message
                };
            }
        }

        public async Task<Resultado> AgregarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad)
        {
            try
            {
                int usuarioIdInt = int.Parse(usuarioId);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC AgregarProductoCarritoLinea @p0, @p1, @p2",
                    usuarioIdInt, productoId, cantidad
                );

                return new Resultado
                {
                    Success = true,
                    Message = "Producto agregado correctamente"
                };
            }
            catch (Exception ex)
            {
                // Podés loguear el error si querés
                return new Resultado
                {
                    Success = false,
                    Message = "Ocurrió un error al agregar el producto: " + ex.Message
                };
            }
        }


        private async Task<UsuarioCarritoResultado> ValidarUsuarioCarritoStock(string usuarioId, int productoId, int cantidad)
        {
            var resultado = new UsuarioCarritoResultado();

            if (usuarioId == null)
            {
                resultado.Success = false;
                resultado.Message = "Error en el usuario";
                return resultado;
            }

            bool disponible = await _productosService.ConsultarDisponibilidad(productoId, cantidad);

            if (!disponible)
            {
                resultado.Success = false;
                resultado.Message = "No hay suficiente stock";
                return resultado;
            }

            resultado.Carrito = await ObtenerCarrito(usuarioId);
            resultado.Success = true;

            return resultado;
        }

        public async Task<Carrito?> ObtenerCarrito(string usuarioId)
        {
            int usuarioIdInt = int.Parse(usuarioId);
            var carrito = await _context.Carritos
                .Include(x => x.Lineas)
                .FirstOrDefaultAsync(c => c.IdUsuario == usuarioIdInt && c.Activo);

            return carrito;
        }

        public async Task EliminarProductoCarrito(string usuarioId, int productoId)
        {
            var carrito = await ObtenerCarrito(usuarioId);

            if(carrito != null)
            {
                var linea = carrito.Lineas.FirstOrDefault(x => x.ProductoId == productoId);
                if (linea != null)
                {
                    _context.CarritoLineas.Remove(linea);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<int> ObtenerTotalItems(string usuarioId)
        {
            var carrito = await ObtenerCarrito(usuarioId);

            return await _context.CarritoLineas
                .Where(l => l.CarritoId == carrito.Id)
                .SumAsync(l => l.Cantidad);
        }

        public async Task<CarritoLinea> ObtenerLineaCarrito(int lineaId)
        {
            return await _context.CarritoLineas.FindAsync(lineaId);
        }

        public async Task<Dictionary<int, ProductoEnCarritoDto>> ObtenerProductosDelCarrito(int usuarioId)
        {
            var carrito = await ObtenerCarrito(usuarioId.ToString());

            if (carrito == null)
                return new Dictionary<int, ProductoEnCarritoDto>();

            return await _context.CarritoLineas
                .Where(cl => cl.CarritoId == carrito.Id)
                .Select(cl => new ProductoEnCarritoDto
                {
                    ProductoId = cl.ProductoId,
                    Cantidad = cl.Cantidad,
                    CarritoLineaId = cl.Id,
                    PrecioUnitario = cl.Producto != null ? cl.Producto.Precio : 0
                })
                .ToDictionaryAsync(cl => cl.ProductoId);
        }

        public async Task<CarritoViewModel> ObtenerCarritoUsuario(string usuarioId)
        {
            var carrito = await ObtenerCarrito(usuarioId);

            if (carrito == null)
                return new CarritoViewModel(); // o lanzar excepción, según tu flujo

            // Cargar las líneas con los productos
            var lineas = await _context.CarritoLineas
                .Include(l => l.Producto)
                .Where(l => l.CarritoId == carrito.Id)
                .ToListAsync();

            // Filtrar líneas sin stock
            var lineasSinStock = lineas
                .Where(l => l.Producto == null || l.Producto.Stock <= 0)
                .ToList();

            if (lineasSinStock.Any())
            {
                _context.CarritoLineas.RemoveRange(lineasSinStock);
                await _context.SaveChangesAsync();

                // Actualizar lista sin las líneas eliminadas
                lineas = lineas.Except(lineasSinStock).ToList();
            }

            var viewModel = new CarritoViewModel
            {
                CarritoId = carrito.Id,
                Lineas = lineas.Select(l => new CarritoLineaViewModel
                {
                    LineaId = l.Id,
                    ProductoId = l.ProductoId,
                    Nombre = l.Producto.Nombre,
                    Precio = l.Producto.Precio,
                    Cantidad = l.Cantidad,
                    Subtotal = l.Cantidad * l.Producto.Precio,
                    ImagenUrl = l.Producto.ImgUrl
                }).ToList()
            };

            viewModel.Total = viewModel.Lineas.Sum(l => l.Subtotal);

            return viewModel;
        }


        public async Task<Resultado> Eliminar(string usuarioId)
        {
            var resultado = new Resultado();

            var carritoActualUsuario = await ObtenerCarrito(usuarioId);

            if (carritoActualUsuario == null)
            {
                resultado.Success = false;
                resultado.Message = "No se encontró un carrito activo para el usuario.";
                return resultado;
            }

            carritoActualUsuario.Activo = false;
            _context.Carritos.Update(carritoActualUsuario);
            await _context.SaveChangesAsync();

            resultado.Success = true;
            resultado.Message = "Carrito eliminado correctamente.";
            return resultado;
        }

    }

}

