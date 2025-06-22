using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueseriaSoftware.Models
{
    public class UsuarioDireccion : EntityBase
    {
        public bool Activo { get; set; }
        public int IdUsuario { get; set; }
        public int IdDireccion { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }
        [ForeignKey("IdDireccion")]
        public Direccion? Direccion { get; set; }
    }
}
