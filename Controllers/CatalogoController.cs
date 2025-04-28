using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Controllers
{
    public class CatalogoController : Controller
    {
        private ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        public async Task<IActionResult> Index(string busqueda)
        {
            AgregarProductoViewModel catalogo = new AgregarProductoViewModel
            {
                Busqueda = busqueda,
                Productos = await _catalogoService.ConsultarCatalogo(),
            };
            return View(catalogo);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarDisponible(int productoId, int cantidad)
        {
            // Obtener stock actual del producto
            var stockDisponible = await _catalogoService.ConsultarDisponibilidad(productoId);

            return Json(new
            {
                disponible = cantidad <= stockDisponible,
                stockDisponible = stockDisponible
            });
        }
    }
}
