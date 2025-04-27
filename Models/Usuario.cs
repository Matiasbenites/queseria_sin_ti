using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using QueseriaSoftware.Models.EntityConfigs;

namespace QueseriaSoftware.Models
{
    public class Usuario : AuditableEntity
    {
        public required string Email { get; set; }
        public required string NombreDeUsuario { get; set; }
        public required string Password { get; set; }
        public required string Dni { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Telefono { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
    }
}
