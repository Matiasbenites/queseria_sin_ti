using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Controllers
{
    public class PedidosController : Controller
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
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
