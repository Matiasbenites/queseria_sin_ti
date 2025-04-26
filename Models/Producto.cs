using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.Models
{
    public class Producto : AuditableEntity
    {
        [Required]
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public string? ImgUrl { get; set; }
    }
}
