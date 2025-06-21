
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;

namespace QueseriaSoftware.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
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
