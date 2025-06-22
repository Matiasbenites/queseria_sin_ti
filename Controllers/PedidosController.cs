using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.Models;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPedidoService _pedidoService;

        public PedidosController(AppDbContext context, IHttpContextAccessor httpContextAccessor, IPedidoService pedidoService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> CrearPedido()
        {
            var usuarioId = User.Identity.IsAuthenticated
            ? User.FindFirst(ClaimTypes.NameIdentifier).Value
            : HttpContext.Session.Id;

            var resultadoCrearPedido = await _pedidoService.CrearPedido(usuarioId, "Direccion pendiente");

            if (resultadoCrearPedido.PedidoPendienteDePago)
            {
                return RedirectToAction("ConfirmarPago", "Pago");
            }

            var viewModel = new SeleccionDireccionViewModel
            {
                TotalCompra = resultadoCrearPedido.Total,
                Direcciones = resultadoCrearPedido.Direcciones,
                Localidades = await _context.Localidades
                .Include(l => l.Provincia)
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = $"{l.Nombre}, {l.Provincia.Nombre}"
                })
                .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDireccionAlPedido(int direccionId)
        {
            var usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            var resultado = await _pedidoService.AgregarDireccionAlPedido(usuarioId, direccionId);

            if (!resultado.Success)
                return BadRequest(new { success = false, message = "No se pudo asociar la dirección al pedido." });

            return Ok(new { success = true, message = "Dirección asociada correctamente." });
        }


        // GET: Pedidos ver pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = _context.Pedidos.Include(p => p.Usuario);
            return View(await pedidos.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", pedido.IdUsuario);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Total,IdUsuario,Estado")] Pedido pedido)
        {
            if (id != pedido.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Nombre", pedido.IdUsuario);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null) return NotFound();

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Cambiar Estado del Pedido usando el patrón State
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound();

            var estadoActual = pedido.EstadoPedido;
            var nuevoEstado = estadoActual.SiguienteEstado();

            pedido.EstadoPedido = nuevoEstado; // cambia estado lógico
            pedido.Estado = nuevoEstado.ObtenerEstado(); // actualiza el campo que persiste en la BD

            _context.Update(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
