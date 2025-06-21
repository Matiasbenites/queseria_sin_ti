using QueseriaSoftware.Models.EntityConfigs;
using System.ComponentModel.DataAnnotations.Schema;

namespace QueseriaSoftware.Models
{
    public class Provincia : EntityBase
    {
        public required string Nombre { get; set; }

        public bool Activo { get; set; }

        public int IdPais { get; set; }

        [ForeignKey("IdPais")]
        public Pais? Pais { get; set; }
    }
}