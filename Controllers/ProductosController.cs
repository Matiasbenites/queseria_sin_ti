using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Models;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    [Authorize(Roles = "Cliente, Admin")]
    public class ProductosController : Controller
    {
        private readonly IProductosService _productosService;
        private readonly ICarritoService _carritoService;

        public ProductosController(IProductosService productosService, ICarritoService carritoService)
        {
            _productosService = productosService;
            _carritoService = carritoService;
        }

        public async Task<IActionResult> Catalogo()
        {
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            CatalogoProductosViewModel catalogo = new CatalogoProductosViewModel
            {
                Productos = await _productosService.ConsultarCatalogo(usuarioId),
            };

            // Obtener productos en el carrito del usuario como diccionario
            var productosEnCarrito = await _carritoService.ObtenerProductosDelCarrito(int.Parse(usuarioId));

            _productosService.ActualizarProductosConEstadoDeCarrito(catalogo.Productos, productosEnCarrito);

            return View(catalogo);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultarDisponible(int productoId, int cantidad)
        {
            if (cantidad <= 0)
            {
                return Ok(new
                {
                    stockDisponible = false,
                    message = "Por favor indique cantidad"
                });
            }
            // Obtener stock actual del producto
            var disponible = await _productosService.ConsultarDisponibilidad(productoId, cantidad);

            return Ok(new
            {
                stockDisponible = disponible,
                message = "No hay suficiente stock"
            });
        }


    }
}
