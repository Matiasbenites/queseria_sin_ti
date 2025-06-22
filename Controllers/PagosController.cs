using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Data;
using QueseriaSoftware.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QueseriaSoftware.Controllers
{
    public class PagosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPagoService _pagoService;

        public PagosController(AppDbContext context, IPagoService pagoService)
        {
            _context = context;
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDatosDePago()
        {
            var usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            var resultadoDatosDePago = await _pagoService.ObtenerDatosDePago(usuarioId);

            if (resultadoDatosDePago == null || !resultadoDatosDePago.Success || resultadoDatosDePago.ConfirmarPedidoViewModel == null)
            {
                TempData["Error"] = "No se pudieron obtener los datos de pago.";
                return RedirectToAction("CrearPedido", "Pedido");
            }

            return View(resultadoDatosDePago.ConfirmarPedidoViewModel);
        }

    }
}
