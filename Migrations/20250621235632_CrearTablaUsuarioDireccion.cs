using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaUsuarioDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Localidad_IdLocalidad",
                table: "Direccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Localidad_Provincia_IdProvincia",
                table: "Localidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Direccion_IdDireccion",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pago_IdPago",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Provincia_Pais_IdPais",
                table: "Provincia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provincia",
                table: "Provincia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pais",
                table: "Pais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pago",
                table: "Pago");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localidad",
                table: "Localidad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion");

            migrationBuilder.RenameTable(
                name: "Provincia",
                newName: "Provincias");

            migrationBuilder.RenameTable(
                name: "Pais",
                newName: "Paises");

            migrationBuilder.RenameTable(
                name: "Pago",
                newName: "Pagos");

            migrationBuilder.RenameTable(
                name: "Localidad",
                newName: "Localidades");

            migrationBuilder.RenameTable(
                name: "Direccion",
                newName: "Direcciones");

            migrationBuilder.RenameIndex(
                name: "IX_Provincia_IdPais",
                table: "Provincias",
                newName: "IX_Provincias_IdPais");

            migrationBuilder.RenameIndex(
                name: "IX_Localidad_IdProvincia",
                table: "Localidades",
                newName: "IX_Localidades_IdProvincia");

            migrationBuilder.RenameIndex(
                name: "IX_Direccion_IdLocalidad",
                table: "Direcciones",
                newName: "IX_Direcciones_IdLocalidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provincias",
                table: "Provincias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Paises",
                table: "Paises",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localidades",
                table: "Localidades",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsuarioDirecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdDireccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioDirecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioDirecciones_Direcciones_IdDireccion",
                        column: x => x.IdDireccion,
                        principalTable: "Direcciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioDirecciones_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDirecciones_IdDireccion",
                table: "UsuarioDirecciones",
                column: "IdDireccion");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioDirecciones_IdUsuario",
                table: "UsuarioDirecciones",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Localidades_IdLocalidad",
                table: "Direcciones",
                column: "IdLocalidad",
                principalTable: "Localidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Localidades_Provincias_IdProvincia",
                table: "Localidades",
                column: "IdProvincia",
                principalTable: "Provincias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Direcciones_IdDireccion",
                table: "Pedidos",
                column: "IdDireccion",
                principalTable: "Direcciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos",
                column: "IdPago",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provincias_Paises_IdPais",
                table: "Provincias",
                column: "IdPais",
                principalTable: "Paises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Localidades_IdLocalidad",
                table: "Direcciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Localidades_Provincias_IdProvincia",
                table: "Localidades");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Direcciones_IdDireccion",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Provincias_Paises_IdPais",
                table: "Provincias");

            migrationBuilder.DropTable(
                name: "UsuarioDirecciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provincias",
                table: "Provincias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Paises",
                table: "Paises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pagos",
                table: "Pagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Localidades",
                table: "Localidades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Direcciones",
                table: "Direcciones");

            migrationBuilder.RenameTable(
                name: "Provincias",
                newName: "Provincia");

            migrationBuilder.RenameTable(
                name: "Paises",
                newName: "Pais");

            migrationBuilder.RenameTable(
                name: "Pagos",
                newName: "Pago");

            migrationBuilder.RenameTable(
                name: "Localidades",
                newName: "Localidad");

            migrationBuilder.RenameTable(
                name: "Direcciones",
                newName: "Direccion");

            migrationBuilder.RenameIndex(
                name: "IX_Provincias_IdPais",
                table: "Provincia",
                newName: "IX_Provincia_IdPais");

            migrationBuilder.RenameIndex(
                name: "IX_Localidades_IdProvincia",
                table: "Localidad",
                newName: "IX_Localidad_IdProvincia");

            migrationBuilder.RenameIndex(
                name: "IX_Direcciones_IdLocalidad",
                table: "Direccion",
                newName: "IX_Direccion_IdLocalidad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provincia",
                table: "Provincia",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pais",
                table: "Pais",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pago",
                table: "Pago",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Localidad",
                table: "Localidad",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Direccion",
                table: "Direccion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Localidad_IdLocalidad",
                table: "Direccion",
                column: "IdLocalidad",
                principalTable: "Localidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Localidad_Provincia_IdProvincia",
                table: "Localidad",
                column: "IdProvincia",
                principalTable: "Provincia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Direccion_IdDireccion",
                table: "Pedidos",
                column: "IdDireccion",
                principalTable: "Direccion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pago_IdPago",
                table: "Pedidos",
                column: "IdPago",
                principalTable: "Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provincia_Pais_IdPais",
                table: "Provincia",
                column: "IdPais",
                principalTable: "Pais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
