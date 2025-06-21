using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    [Authorize(Roles = "Cliente, Admin")]
    public class CarritoController : Controller
    {

        private readonly ICarritoService _carritoService;
        private readonly IPedidoService _pedidoService;

        public CarritoController(ICarritoService carritoService, IPedidoService pedidoService)
        {
            _carritoService = carritoService;
            _pedidoService = pedidoService;
        }


        public async Task<IActionResult> ObtenerCarritoUsuario()
        {
            string usuarioId = User.Identity.IsAuthenticated
            ? User.FindFirst(ClaimTypes.NameIdentifier).Value
            : HttpContext.Session.Id;
            var carrito = await _carritoService.ObtenerCarritoUsuario(usuarioId);
            carrito.estadoUltimoPedido = await _pedidoService.ObtenerEstadoUltimoPedido(usuarioId);
            return View(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(int productoId, int cantidad)
        {
            // Obtener ID del usuario actual
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;
            if(cantidad >= 0)
            {
                return Json(new
                {
                    success = false,
                    message = "Por favor indique cantidad",
                });
            }
            // Agregar al carrito
            var result = await _carritoService.AgregarProducto(usuarioId, productoId, cantidad);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ModificarCantidadProducto(int productoId, int cantidad, string returnUrl)
        {
            string usuarioId = User.Identity.IsAuthenticated
            ? User.FindFirst(ClaimTypes.NameIdentifier).Value
            : HttpContext.Session.Id;

            // Actualizar cantidad
            await _carritoService.ModificarCantidadProducto(usuarioId, productoId, cantidad);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Productos");
            //return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarProductoCarrito(int productoId, string returnUrl)
        {
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            await _carritoService.EliminarProductoCarrito(usuarioId, productoId);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Catalogo");            
            //return Json(new { success = true });
        }
    }
}
