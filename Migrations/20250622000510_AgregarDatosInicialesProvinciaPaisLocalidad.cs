using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosInicialesProvinciaPaisLocalidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "Id", "Activo", "Nombre" },
                values: new object[] { 1, true, "Argentina" });

            migrationBuilder.InsertData(
                table: "Provincias",
                columns: new[] { "Id", "Activo", "IdPais", "Nombre" },
                values: new object[,]
                {
                    { 1, true, 1, "Corrientes" },
                    { 2, true, 1, "Chaco" }
                });

            migrationBuilder.InsertData(
                table: "Localidades",
                columns: new[] { "Id", "Activo", "CodigoPostal", "IdProvincia", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "3400", 1, "Corrientes Capital" },
                    { 2, true, "3500", 2, "Resistencia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Localidades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Localidades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Provincias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Provincias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
