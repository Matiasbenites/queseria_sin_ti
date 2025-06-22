using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Localidades
{
    public class LocalidadConfiguracion : IEntityTypeConfiguration<Localidad>
    {
        public void Configure(EntityTypeBuilder<Localidad> builder)
        {
            builder.HasData(
                new Localidad
                {
                    Id = 1,
                    Nombre = "Corrientes Capital",
                    Activo = true,
                    CodigoPostal = "3400",
                    IdProvincia = 1
                },
                new Localidad
                {
                    Id = 2,
                    Nombre = "Resistencia",
                    Activo = true,
                    CodigoPostal = "3500",
                    IdProvincia = 2
                }
            );
        }
    }
}
