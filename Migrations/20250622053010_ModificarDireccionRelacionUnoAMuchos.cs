using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class ModificarDireccionRelacionUnoAMuchos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioDirecciones");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Direcciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_IdUsuario",
                table: "Direcciones",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Direcciones_Usuarios_IdUsuario",
                table: "Direcciones",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direcciones_Usuarios_IdUsuario",
                table: "Direcciones");

            migrationBuilder.DropIndex(
                name: "IX_Direcciones_IdUsuario",
                table: "Direcciones");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Direcciones");

            migrationBuilder.CreateTable(
                name: "UsuarioDirecciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDireccion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
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
        }
    }
}
