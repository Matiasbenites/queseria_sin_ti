using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueseriaSoftware.Models
{
    public class Carrito : AuditableEntity
    {
        public required int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
    }
}
