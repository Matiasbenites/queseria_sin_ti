﻿using Microsoft.AspNetCore.Mvc;
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

            if (!resultadoDatosDePago.Success || resultadoDatosDePago.ConfirmarPedidoViewModel == null)
            {
                ViewBag.Error = resultadoDatosDePago.Message ?? "No se pudieron obtener los datos de pago.";

                return View("ObtenerDatosDePago", null);
            }

            return View(resultadoDatosDePago.ConfirmarPedidoViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ProcesarPago(int medioPagoId)
        {
            var usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            var resultadoPago = await _pagoService.ProcesarPago(medioPagoId, usuarioId);

            return Json(new
            {
                success = resultadoPago.Success,
                message = resultadoPago.Message
            });
        }


    }
}
