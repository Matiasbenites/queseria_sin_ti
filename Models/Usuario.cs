using QueseriaSoftware.Models.EntityConfigs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models
{
    public class Usuario : AuditableEntity
    {
        // No redeclarar Id, ya viene de AuditableEntity

        public required string Email { get; set; }
        public required string NombreDeUsuario { get; set; }
        public required string Password { get; set; }
        public required string Dni { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public string? Telefono { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
        public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
        public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();
    }
}
