using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public required int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }

        public required int IdMarca { get; set; }

        [ForeignKey("IdMarca")]
        public Marca? Marca { get; set; }
    }
}
