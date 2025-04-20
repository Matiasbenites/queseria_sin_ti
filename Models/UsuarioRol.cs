using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using QueseriaSoftware.Models.EntityConfigs;

namespace QueseriaSoftware.Models
{
    public class UsuarioRol : EntityBase
    {
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }
    }
}