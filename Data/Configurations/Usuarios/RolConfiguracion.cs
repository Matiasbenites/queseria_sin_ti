using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Usuarios
{
    public class RolConfiguracion : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Descripcion).HasMaxLength(250);

            builder.HasData(new Rol
            {
                Id = 1,
                Nombre = "Admin",
                Descripcion = "Rol de administrador",
                CreadoEn = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Activo = true
            });
        }
    }
}
