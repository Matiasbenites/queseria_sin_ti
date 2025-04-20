using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Usuarios
{
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Dni).IsRequired();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(250);
            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
