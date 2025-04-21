using QueseriaSoftware.Data;
using QueseriaSoftware.Models;
using System.Security.Cryptography;
using System.Text;

namespace QueseriaSoftware.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly AppDbContext _context;

        public UsuariosService(AppDbContext context)
        {
            _context = context;
        }

        public Usuario? Autenticar(string nombreUsuario, string password)
        {
            var passwordHash = CalcularHash(password);
            return _context.Usuarios
                .FirstOrDefault(u => u.NombreDeUsuario == nombreUsuario && u.Password == passwordHash);
        }

        private string CalcularHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
