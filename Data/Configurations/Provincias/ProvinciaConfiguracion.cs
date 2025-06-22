using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Provincias
{
    public class ProvinciaConfiguracion : IEntityTypeConfiguration<Provincia>
    {
        public void Configure(EntityTypeBuilder<Provincia> builder)
        {
            builder.HasData(
                new Provincia
                {
                    Id = 1,
                    Nombre = "Corrientes",
                    Activo = true,
                    IdPais = 1
                },
                new Provincia
                {
                    Id = 2,
                    Nombre = "Chaco",
                    Activo = true,
                    IdPais = 1
                }
            );
        }
    }
}
