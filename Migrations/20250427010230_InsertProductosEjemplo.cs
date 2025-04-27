using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class InsertProductosEjemplo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CreadoEn", "Descripcion", "IdCategoria", "IdMarca", "ImgUrl", "ModificadoEn", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Queso holandés suave y cremoso", 1, 1, "/images/queso-gouda.jpg", null, "Queso Gouda", 12.50m, 0 },
                    { 3, true, new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Queso italiano duro de leche de vaca, perfecto para pastas", 1, 1, "/images/queso-parmesano.jpg", null, "Queso Parmesano", 18.75m, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
