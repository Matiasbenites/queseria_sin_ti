using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QueseriaSoftware.Models.EntityConfigs;

namespace QueseriaSoftware.Models
{
    public class Direccion : EntityBase
    {
        [Required]
        public required string Calle { get; set; }

        public int Numero { get; set; }

        public bool Activo { get; set; }

        [Required]
        public required string TelefonoContacto { get; set; }

        [Required]
        public int IdLocalidad {  get; set; }

        [ForeignKey("IdLocalidad")]
        public Localidad? Localidad { get; set; }
    }
}