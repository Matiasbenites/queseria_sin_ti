using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pagos;
using QueseriaSoftware.Models.EstadosPedido;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProductosService _productosService;
        private readonly AppDbContext _context;


        public PagoService(IPedidoService pedidoService, AppDbContext context, IProductosService productosService)
        {
            _pedidoService = pedidoService;
            _context = context;
            _productosService = productosService;
        }

        public async Task<ResultadoDatosDePago> ObtenerDatosDePago(string usuarioId)
        {
            ResultadoDatosDePago resultado = new ResultadoDatosDePago();
            var estadoUltimoPedido = await _pedidoService.ObtenerEstadoUltimoPedido(usuarioId);

            if (string.IsNullOrEmpty(estadoUltimoPedido) || !estadoUltimoPedido.Equals("Nuevo")) 
            {
                resultado.Success = false;
                resultado.Message = "Aun no hiciste un pedido";
            }

            resultado.ConfirmarPedidoViewModel.Total = await _pedidoService.CalcularTotalPedido(usuarioId); ;
            resultado.ConfirmarPedidoViewModel.Pagos = await ObtenerMediosPago();

            resultado.Success = true;
            return resultado;
        }

        public async Task<List<PagosViewModel>> ObtenerMediosPago()
        {
            return await _context.Pagos
                .Where(x => x.Activo)
                .Select(x => new PagosViewModel
                {
                    Nombre = x.Nombre,
                    Id = x.Id,
                })
                .ToListAsync();
        }

        public async Task<ResultadoConfirmarPedido> ProcesarPago(int medioPagoId, string usuarioId)
        {
            var resultado = new ResultadoConfirmarPedido();

            var validacion = await ValidarDatosPago(medioPagoId, usuarioId);
            if (!validacion.Success)
            {
                resultado.Success = false;
                resultado.Message = validacion.Message;
                return resultado;
            }

            var ultimoPedido = await _pedidoService.ObtenerUltimoPedido(usuarioId);
            var productosUltimoPedido = ultimoPedido.PedidoDetalles;

            var resultadoStock = await _productosService.DescontarStock(productosUltimoPedido);
            if (!resultadoStock.Success)
            {
                resultado.Success = false;
                resultado.Message = resultadoStock.Message;
                return resultado;
            }



            resultado.Success = true;
            resultado.Message = "Pedido confirmado correctamente.";
            return resultado;
        }


        private async Task<Resultado> ValidarDatosPago(int medioPagoId, string usuarioId)
        {
            var resultado = new Resultado();

            // Validar usuario
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                resultado.Success = false;
                resultado.Message = "Usuario no válido.";
                return resultado;
            }

            // Validar medio de pago
            var medioPago = await _context.Pagos.FindAsync(medioPagoId);
            if (medioPago == null)
            {
                resultado.Success = false;
                resultado.Message = "Medio de pago no válido.";
                return resultado;
            }

            // Validar que haya un pedido en curso
            var pedido = await _context.Pedidos
            .Include(p => p.PedidoDetalles)
                .FirstOrDefaultAsync(p => p.IdUsuario == int.Parse(usuarioId) && p.Estado == "Nuevo");

            if (pedido == null)
            {
                resultado.Success = false;
                resultado.Message = "Aun no hiciste un pedido";
                return resultado;
            }

            if (pedido.PedidoDetalles == null || !pedido.PedidoDetalles.Any())
            {
                resultado.Success = false;
                resultado.Message = "El pedido no tiene productos.";
                return resultado;
            }

            resultado.Success = true;
            return resultado;
        }

    }
}
