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

            if (validacionCarrito.Carrito == null || !validacionCarrito.Success)
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

            if (validacionCarrito.Carrito == null || !validacionCarrito.Success)
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
          
            // Verificar si el producto ya está en el carrito
            var productoEnCarrito = carrito.Lineas
                .FirstOrDefault(l => l.CarritoId == carrito.Id && l.ProductoId == productoId);

            if (productoEnCarrito != null)
            {
                // Actualizar cantidad si ya existe
                productoEnCarrito.Cantidad += cantidad;
            }
            else
            {
                // Agregar nueva línea si no existe
                var nuevaLinea = new CarritoLinea
                {
                    CarritoId = carrito.Id,
                    ProductoId = productoId,
                    Cantidad = cantidad,
                };

                _context.CarritoLineas.Add(nuevaLinea);
            }

            await _context.SaveChangesAsync();
            return new Resultado
            {
                Success = true,
                Message = "Carrito actualizado"
            };
        }

        public async Task<Resultado> AgregarProductoCarritoLinea(Carrito? carrito, string usuarioId, int productoId, int cantidad)
        {
            if (carrito == null)
            {
                // Si el carrito no existe, se crea uno y se agrega
                carrito = new Carrito
                {
                    IdUsuario = int.Parse(usuarioId),
                    CreadoEn = DateTime.Now,
                    Activo = true,
                    ModificadoEn = DateTime.Now,
                };

                _context.Carritos.Add(carrito);

                //No existe nueva linea, agregamos al carrito.
                var nuevaLinea = new CarritoLinea
                {
                    CarritoId = carrito.Id,
                    ProductoId = productoId,
                    Cantidad = cantidad,
                };

                _context.CarritoLineas.Add(nuevaLinea);
                await _context.SaveChangesAsync();
                return new Resultado
                {
                    Success = true,
                    Message = "Producto agregado correctamente"
                };
            }

            //Si el carrito existe, se actualiza la linea
            return await ActualizarProductoCarritoLinea(carrito, usuarioId, productoId, cantidad);
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
            return await _context.CarritoLineas
                .Where(cl => cl.Carrito.IdUsuario == usuarioId)
                .Select(cl => new ProductoEnCarritoDto
                {
                    ProductoId = cl.ProductoId,
                    Cantidad = cl.Cantidad,
                    CarritoLineaId = cl.Id
                })
                .ToDictionaryAsync(cl => cl.ProductoId);
        }

        public async Task<CarritoViewModel> ObtenerCarritoUsuario(string usuarioId)
        {
            var carrito = await ObtenerCarrito(usuarioId);

            var lineas = await _context.CarritoLineas
                .Include(l => l.Producto)
                .Where(l => l.CarritoId == carrito.Id)
                .ToListAsync();

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
    }

}

