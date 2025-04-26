using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Productos
{
    public class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasData(
                new Categoria
                {
                    Id = 1,
                    Nombre = "Quesos Duros",
                    Descripcion = "Quesos de pasta dura madurados",
                    Activo = true
                }
            );
        }
    }
}
