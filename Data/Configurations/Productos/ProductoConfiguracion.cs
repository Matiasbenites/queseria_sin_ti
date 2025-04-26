using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Data.Configurations.Productos
{
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {

            builder.HasData(
            new Producto
            {
                Id = 1,
                Nombre = "Queso Manchego",
                Descripcion = "Queso tradicional español de leche de oveja",
                Precio = 15.99m,
                Stock = 100,
                ImgUrl = "/images/queso-manchego.jpg",
                IdCategoria = 1,
                CreadoEn = new DateTime(2024, 4, 26, 0, 0, 0, DateTimeKind.Utc),
                IdMarca = 1,
                Activo = true,
            }
        );
        }
    }
}
