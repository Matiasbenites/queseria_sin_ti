using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueseriaSoftware.Models
{
    public class Localidad : EntityBase
    {
        public required string Nombre { get; set; }

        public bool Activo { get; set; }

        public string? CodigoPostal { get; set; }

        public int IdProvincia { get; set; }

        [ForeignKey("IdProvincia")]
        public Provincia? Provincia { get; set; }
    }
}