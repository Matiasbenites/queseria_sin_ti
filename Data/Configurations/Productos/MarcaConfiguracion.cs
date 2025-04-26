using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Productos
{
    public class MarcaConfiguracion : IEntityTypeConfiguration<Marca>
    {
        public void Configure(EntityTypeBuilder<Marca> builder)
        {
            builder.HasData(
                new Marca
                {
                    Id = 1,
                    Nombre = "Quesería Artesanal",
                    Descripcion = "Quesería tradicional con productos artesanales",
                    Activo = true
                }
            );
        }
    }
}
