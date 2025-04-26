using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class InsertProductoMarcaCategoriaDePrueba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Activo", "Descripcion", "Nombre" },
                values: new object[] { 1, true, "Quesos de pasta dura madurados", "Quesos Duros" });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "Id", "Activo", "Descripcion", "Nombre" },
                values: new object[] { 1, true, "Quesería tradicional con productos artesanales", "Quesería Artesanal" });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CreadoEn", "Descripcion", "IdCategoria", "IdMarca", "ImgUrl", "ModificadoEn", "Nombre", "Precio", "Stock" },
                values: new object[] { 1, true, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Queso tradicional español de leche de oveja", 1, 1, "/images/queso-manchego.jpg", null, "Queso Manchego", 15.99m, 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Marca",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
