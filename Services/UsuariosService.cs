using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
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
    }
}
