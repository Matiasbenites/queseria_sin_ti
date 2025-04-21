using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Usuarios
{
    /// <summary>
    /// Configuración de la entidad intermedia UsuarioRol que representa 
    /// la relación muchos a muchos entre Usuario y Rol.
    /// </summary>
    public class UsuarioRolConfiguracion : IEntityTypeConfiguration<UsuarioRol>
    {
        /// <summary>
        /// Configura la entidad UsuarioRol para establecer claves primarias 
        /// y relaciones con las entidades Usuario y Rol.
        /// </summary>
        /// <param name="builder">Constructor de entidad proporcionado por EF Core.</param>
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            /// Establece la clave primaria de la tabla UsuarioRol.
            builder.HasKey(ur => ur.Id);

            /// Configura la relación muchos a uno con Usuario:
            /// Un Usuario puede tener muchos roles.
            builder.HasOne(ur => ur.Usuario)
                   .WithMany(u => u.UsuarioRoles)
                   .HasForeignKey(ur => ur.IdUsuario);

            /// Configura la relación muchos a uno con Rol:
            /// Un Rol puede estar asignado a muchos usuarios.
            builder.HasOne(ur => ur.Rol)
                   .WithMany(r => r.UsuarioRoles)
                   .HasForeignKey(ur => ur.IdRol);

            builder.HasData(new UsuarioRol
            {
                Id = 1,
                IdUsuario = 1,
                IdRol = 1,
                Activo = true
            });
        }
    }
}
