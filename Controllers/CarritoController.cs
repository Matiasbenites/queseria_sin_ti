using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    public class CarritoController : Controller
    {

        private readonly ICatalogoService _catalogoService;
        private readonly ICarritoService _carritoService;

        public CarritoController(ICatalogoService catalogoService, ICarritoService carritoService)
        {
            _catalogoService = catalogoService;
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
            if (cantidad <= 0)
            {
                return Json(new
                {
                    success = false,
                    message = $"Por favor ingrese una cantidad de productos a agregar"
                });
            }
            // Verificar stock antes de agregar al carrito
            var stockDisponible = await _catalogoService.ConsultarDisponibilidad(productoId);

            if (cantidad > stockDisponible)
            {
                return Json(new
                {
                    success = false,
                    message = $"No hay suficiente stock. Stock disponible: {stockDisponible}"
                });
            }

            // Obtener ID del usuario actual
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            // Agregar al carrito
            await _carritoService.AgregarProductoCarrito(usuarioId, productoId, cantidad);


            return Json(new
            {
                success = true,
                message = "Producto agregado al carrito correctamente",
            });
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCantidad(int lineaId, int cantidad, string returnUrl)
        {
            // Verificar stock antes de actualizar
            var linea = await _carritoService.ObtenerLineaCarrito(lineaId);
            var stockDisponible = await _catalogoService.ConsultarDisponibilidad(linea.ProductoId);

            if (cantidad > stockDisponible)
            {
                return Json(new
                {
                    success = false,
                    message = $"No hay suficiente stock. Stock disponible: {stockDisponible}"
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
