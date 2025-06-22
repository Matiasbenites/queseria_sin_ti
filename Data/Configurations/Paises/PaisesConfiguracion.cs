using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Paises
{
    public class PaisesConfiguracion : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.HasData(
                 new Pais
                 {
                     Id = 1,
                     Nombre = "Argentina",
                     Activo = true
                 }
             );
        }
    }
}
