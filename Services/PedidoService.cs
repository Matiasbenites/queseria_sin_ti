
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs;
using QueseriaSoftware.DTOs.Resultados.Pedido;

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
            var productosEnCarrito = await _carritoService.ObtenerProductosDelCarrito(int.Parse(usuarioId));
            var total = CalcularTotal(productosEnCarrito);
            var direcciones = await _usuariosService.ObtenerDireccionesDelUsuario(usuarioId);

            ResultadoCrearPedido resultado = new ResultadoCrearPedido();
            resultado.Total = total;
            resultado.Direcciones = direcciones;
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
    }
}
