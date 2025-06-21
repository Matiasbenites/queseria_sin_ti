using QueseriaSoftware.Models.EntityConfigs;

namespace QueseriaSoftware.Models
{
    public class Pais : EntityBase
    {
        public required string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}