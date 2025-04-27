using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public class CatalogoService : ICatalogoService
    {
        private readonly AppDbContext _context;

        public CatalogoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> ConsultarDisponibilidad(int productoId)
        {
            var producto = await _context.Productos.FindAsync(productoId);
            return producto?.Stock ?? 0;
        }

        public async Task<List<ProductoViewModel>> ConsultarCatalogo(string busqueda)
        {
            if (!String.IsNullOrEmpty(busqueda))
            {
                return await _context.Productos
                    .Where(p => p.Activo && p.Stock > 0 && p.Nombre == busqueda)
                    .Select(p => new ProductoViewModel
                    {
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Descripcion = p.Descripcion,
                        Id = p.Id,
                        ImagenUrl = p.ImgUrl != null ? p.ImgUrl : "/imagenes/sin-imagen.jpg",
                        Stock = p.Stock
                    })
                    .ToListAsync();
            }

            return await _context.Productos
                .Where(p => p.Activo && p.Stock > 0)
                .Select(p => new ProductoViewModel
                {
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Descripcion = p.Descripcion,
                    Id = p.Id,
                    ImagenUrl = p.ImgUrl != null ? p.ImgUrl : "/imagenes/sin-imagen.jpg",
                    Stock = p.Stock
                })
                .Take(5)
                .ToListAsync();

        }
    }
}
