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

            // Enlazar cada producto del catalogo con la información del carrito solo si corresponde
            foreach (var producto in catalogo.Productos)
            {
                if (productosEnCarrito.TryGetValue(producto.Id, out var linea))
                {
                    producto.CantidadEnCarrito = linea.Cantidad;
                    producto.CarritoLineaId = linea.CarritoLineaId;
                }
            }

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
