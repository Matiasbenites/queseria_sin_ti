using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pagos;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPedidoService _pedidoService;
        private readonly AppDbContext _context;


        public PagoService(IPedidoService pedidoService, AppDbContext context)
        {
            _pedidoService = pedidoService;
            _context = context;
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
    }
}
