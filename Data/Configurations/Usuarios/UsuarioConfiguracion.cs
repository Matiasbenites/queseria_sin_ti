using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Usuarios
{
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.NombreDeUsuario).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Apellido).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Dni).IsRequired();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(250);
            builder.HasIndex(u => u.Email)
                .IsUnique();
            builder.HasIndex(u => u.NombreDeUsuario)
                .IsUnique();

            builder.HasData(new Usuario
            {
                Id = 1,
                Email = "admin@queseria.com",
                NombreDeUsuario = "admin",
                Password = "2TqRgh1rqZzD1pFbWbdA3MxgNEcdsF7p8hDwvlT0zas=", // SHA256 hash de admin123
                Dni = "12345678",
                Nombre = "Administrador",
                Apellido = "General",
                Telefono = "123456789",
                CreadoEn = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Activo = true
            });
        }
    }
}
