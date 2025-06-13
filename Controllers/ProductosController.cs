using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Controllers
{
    [Authorize(Roles = "Cliente, Admin")]
    public class ProductosController : Controller
    {
        private readonly IProductosService _productosService;

        public ProductosController(IProductosService productosService)
        {
            _productosService = productosService;
        }

        public async Task<IActionResult> Catalogo()
        {
            CatalogoProductosViewModel catalogo = new CatalogoProductosViewModel
            {
                Productos = await _productosService.ConsultarCatalogo(),
            };

            return View(catalogo);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarDisponible(int productoId, int cantidad)
        {
            // Obtener stock actual del producto
            var disponible = await _productosService.ConsultarDisponibilidad(productoId, cantidad);

            return Ok(new
            {
                stockDisponible = disponible
            });
        }


    }
}
