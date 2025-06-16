using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    [Authorize(Roles = "Cliente, Admin")]
    public class CarritoController : Controller
    {

        private readonly IProductosService _productosService;
        private readonly ICarritoService _carritoService;

        public CarritoController(IProductosService productosService, ICarritoService carritoService)
        {
            _productosService = productosService;
            _carritoService = carritoService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Checkout()
        {
            string usuarioId = User.Identity.IsAuthenticated
            ? User.FindFirst(ClaimTypes.NameIdentifier).Value
            : HttpContext.Session.Id;
            var carrito = await _carritoService.ObtenerCarritoCompleto(usuarioId);
            return View(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(int productoId, int cantidad)
        {
            // Obtener ID del usuario actual
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            // Agregar al carrito
            var result = await _carritoService.AgregarProductoCarrito(usuarioId, productoId, cantidad);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
            });
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCantidad(int lineaId, int cantidad, string returnUrl)
        {
            // Verificar stock antes de actualizar
            var linea = await _carritoService.ObtenerLineaCarrito(lineaId);
            var disponible = await _productosService.ConsultarDisponibilidad(linea.ProductoId, cantidad);

            if (!disponible)
            {
                return Json(new
                {
                    success = false,
                    message = $"No hay suficiente stock."
                });
            }

            // Actualizar cantidad
            await _carritoService.ActualizarCantidad(lineaId, cantidad);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Catalogo");
            //return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarLinea(int lineaId, string returnUrl)
        {
            await _carritoService.EliminarLinea(lineaId);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Catalogo");            
            //return Json(new { success = true });
        }
    }
}
