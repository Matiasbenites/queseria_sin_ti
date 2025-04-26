using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models
{
    public class Marca : EntityBase
    {
        [Required]
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
