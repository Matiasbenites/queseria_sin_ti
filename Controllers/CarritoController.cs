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
        private readonly IUsuariosService _usuarioService;

        public CarritoController(IProductosService productosService, ICarritoService carritoService, IUsuariosService usuarioService)
        {
            _productosService = productosService;
            _carritoService = carritoService;
            _usuarioService = usuarioService;
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
