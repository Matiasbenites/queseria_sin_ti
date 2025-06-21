using QueseriaSoftware.Models.EntityConfigs;

namespace QueseriaSoftware.Models
{
    public class Pago : EntityBase
    {
        public required string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}