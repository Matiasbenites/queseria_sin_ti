using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models
{
    public class Rol : AuditableEntity
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}