using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Data;
using QueseriaSoftware.Services;
using System.Security.Claims;

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

        public IActionResult ObtenerDatosDePago()
        {
            var usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            return View();
        }
    }
}
