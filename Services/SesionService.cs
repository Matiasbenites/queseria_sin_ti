using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QueseriaSoftware.Services
{
    public class SesionService : ISesionService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SesionService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioLoginDto?> AutenticarAsync(string nombreUsuario, string password)
        {
            var passwordHash = CalcularHash(password);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreDeUsuario == nombreUsuario && u.Password == passwordHash);

            if (usuario == null)
                return null;


            var roles = await _context.UsuarioRoles
                .Where(ur => ur.IdUsuario == usuario.Id)
                .Select(ur => ur.Rol != null ? ur.Rol.Nombre : "Invitado")
                .ToListAsync();

            var usuarioLogin = new UsuarioLoginDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                NombreDeUsuario = usuario.NombreDeUsuario,
                Roles = roles
            };


            return usuarioLogin;
        }

        public async Task IniciarSesionAsync(UsuarioLoginDto usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreDeUsuario),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            if (usuario.Roles != null && usuario.Roles.Count > 0)
            {
                foreach (var rol in usuario.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol));
                }
            }

            var identidad = new ClaimsIdentity(claims, "QueseriaCookiesAuth");
            var principal = new ClaimsPrincipal(identidad);

            var context = _httpContextAccessor.HttpContext;
            if (context is not null)
            {
                await context.SignInAsync("QueseriaCookiesAuth", principal);
            }
        }

        public async Task CerrarSesionAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context is not null)
            {
                await context.SignOutAsync("QueseriaCookiesAuth");
            }
        }

        public string CalcularHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
