using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QueseriaSoftware.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuariosService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> ValidarUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);

            return usuario != null;
        }


        public string ObtenerUsuarioId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User.Identity?.IsAuthenticated == true)
            {
                return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            return context?.Session?.Id ?? throw new InvalidOperationException("No hay sesión disponible.");
        }

        public bool EsAutenticado()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        public async Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId)
        {
            var idUsuario = int.Parse(usuarioId);
            var direcciones = await _context.UsuarioDirecciones
                .Where(ud => ud.IdUsuario == idUsuario && ud.Activo && ud.Direccion != null && ud.Direccion.Activo)
                .Include(ud => ud.Direccion!)
                    .ThenInclude(d => d.Localidad!)
                        .ThenInclude(l => l.Provincia)
                .Select(ud => ud.Direccion!)
                .ToListAsync();

            var direccionesDto = direcciones.Select(d => new DireccionViewModel
            {
                Id = d.Id,
                Calle = d.Calle,
                Numero = d.Numero,
                TelefonoContacto = d.TelefonoContacto,
                Localidad = new LocalidadViewModel
                {
                    Nombre = d.Localidad!.Nombre,
                    Provincia = new ProvinciaViewModel
                    {
                        Nombre = d.Localidad.Provincia!.Nombre
                    }
                }
            }).ToList();

            return direccionesDto;
        }
    }
}
