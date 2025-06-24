using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;
using Rotativa.AspNetCore;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    public class FacturaController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public FacturaController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> Imprimir()
        {
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            // Obtener el último pedido del usuario
            var pedido = await _pedidoService.ObtenerUltimoPedido(usuarioId);

            if (pedido == null || pedido.Estado.Equals("Nuevo") || pedido.Estado.Equals("Direccion pendiente"))
            {
                return NotFound("No se encontró el último pedido o el estado no es válido.");
            }

            var model = new FacturaViewModel
            {
                PedidoId = pedido.Id,
                Fecha = pedido.Fecha,
                ClienteNombre = pedido.Usuario.Nombre,
                Items = pedido.PedidoDetalles.Select(l => new FacturaItemViewModel
                {
                    Producto = l.Producto.Nombre,
                    Cantidad = l.Cantidad,
                    PrecioUnitario = l.Producto.Precio,
                    Subtotal = l.Cantidad * l.Producto.Precio
                }).ToList(),
                Total = pedido.Total,
                MedioPago = pedido.Pago?.Nombre
            };

            var pdfResult = new ViewAsPdf("Factura", model)
            {
                FileName = $"Factura-{pedido.Id}.pdf",
                ContentDisposition = Rotativa.AspNetCore.Options.ContentDisposition.Inline,
                CustomSwitches = "--disable-smart-shrinking"
            };

            // Generar bytes del PDF
            var pdfBytes = await pdfResult.BuildFile(ControllerContext);

            var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var carpetaFacturas = Path.Combine(wwwrootPath, "facturas");

            if (!Directory.Exists(carpetaFacturas))
            {
                Directory.CreateDirectory(carpetaFacturas);
            }

            var rutaArchivo = Path.Combine(carpetaFacturas, $"Factura-{pedido.Id}.pdf");

            await System.IO.File.WriteAllBytesAsync(rutaArchivo, pdfBytes);

            return pdfResult;

        }

    }

}
