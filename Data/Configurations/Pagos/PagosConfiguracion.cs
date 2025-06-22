using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Pagos
{
    public class PagosConfiguracion : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.HasData(
                 new Pago
                 {
                     Id = 1,
                     Nombre = "Transferencia",
                     Activo = true
                 },
                 new Pago
                 {
                     Id = 2,
                     Nombre = "Efectivo",
                     Activo = true
                 },
                 new Pago
                 {
                     Id = 3,
                     Nombre = "Tarjeta",
                     Activo = false
                 }
             );
        }
    }
}
