
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pedido;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly AppDbContext _context;
        private readonly IUsuariosService _usuariosService;
        private readonly ICarritoService _carritoService;


        public PedidoService(AppDbContext context, IUsuariosService usuariosService, ICarritoService carritoService)
        {
            _context = context;
            _usuariosService = usuariosService;
            _carritoService = carritoService;
        }

        public async Task<ResultadoCrearPedido> CrearPedido(string usuarioId, string estado)
        {
            ResultadoCrearPedido resultado = new ResultadoCrearPedido();

            var estadoUltimoPedido = await ObtenerEstadoUltimoPedido(usuarioId);

            bool crearPedido = true;

            if (!string.IsNullOrEmpty(estadoUltimoPedido) && estadoUltimoPedido.Equals("Nuevo"))
            {
                resultado.PedidoPendienteDePago = true;
                return resultado;
            }

            if (!string.IsNullOrEmpty(estadoUltimoPedido) && estadoUltimoPedido.Equals("Direccion pendiente"))
            {
                crearPedido = false;
                resultado.PedidoConDireccionPendiente = true;
            }

            var productosEnCarrito = await _carritoService.ObtenerProductosDelCarrito(int.Parse(usuarioId));
            var total = CalcularTotal(productosEnCarrito);
            var direcciones = await _usuariosService.ObtenerDireccionesDelUsuario(usuarioId);

            resultado.Total = total;
            resultado.Direcciones = direcciones;

            if (crearPedido)
            {
                // Crear el pedido sin direccion ni pago
                var pedido = new Pedido
                {
                    Fecha = DateTime.UtcNow,
                    ModificadoEn = DateTime.UtcNow,
                    Estado = estado,
                    Total = total,
                    IdUsuario = int.Parse(usuarioId),
                };

                // Agregar los detalles
                foreach (var item in productosEnCarrito.Values)
                {
                    pedido.PedidoDetalles.Add(new PedidoDetalle
                    {
                        IdProducto = item.ProductoId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario
                    });
                }

                // Guardar en la base de datos
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
            }

            return resultado;
        }

        private decimal CalcularTotal(Dictionary<int, ProductoEnCarritoDto> productosEnCarrito)
        {
            return productosEnCarrito.Values.Sum(p => p.Cantidad * p.PrecioUnitario);
        }

        public async Task<string> ObtenerEstadoUltimoPedido(string idUsuario)
        {
            int usuarioId = int.Parse(idUsuario);
            var ultimoPedido = await _context.Pedidos
                .Where(x => x.IdUsuario == usuarioId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (ultimoPedido == null)
            {
                return(string.Empty);
            }
            return ultimoPedido.EstadoPedido.ObtenerEstado();
        }

        public async Task<Resultado> AgregarDireccionAlPedido(string usuarioId, int direccionId)
        {
            var resultadoValidarDireccion = await _usuariosService.ValidarDireccion(usuarioId, direccionId);

            if (!resultadoValidarDireccion.Success)
            {
                return new Resultado
                {
                    Success = false,
                    Message = "La dirección no es válida para este usuario."
                };
            }

            var pedido = await ObtenerUltimoPedido(usuarioId);

            if (pedido == null)
            {
                return new Resultado
                {
                    Success = false,
                    Message = "No se encontró un pedido activo para el usuario."
                };
            }

            pedido.IdDireccion = direccionId;

            CambiarASiguienteEstado(pedido);


            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();

            return new Resultado
            {
                Success = true,
                Message = "Dirección asociada correctamente al pedido."
            };
        }


        public async Task<Pedido?> ObtenerUltimoPedido(string usuarioId)
        {
            var ultimoPedido = await _context.Pedidos
                .Where(x => x.IdUsuario == int.Parse(usuarioId))
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            return ultimoPedido;
        }

        public void CambiarASiguienteEstado(Pedido pedido)
        {
            var estadoActual = pedido.EstadoPedido;
            var nuevoEstado = estadoActual.SiguienteEstado();

            pedido.EstadoPedido = nuevoEstado; // cambia estado lógico
            pedido.Estado = nuevoEstado.ObtenerEstado(); // actualiza el campo que persiste en la BD
        }

        public async Task<decimal> CalcularTotalPedido(string usuarioId)
        {
            var productosEnCarrito = await _carritoService.ObtenerProductosDelCarrito(int.Parse(usuarioId));
            return CalcularTotal(productosEnCarrito);
        }
    }
}
