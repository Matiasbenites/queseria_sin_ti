using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class CrearTablaPagoYFkEnPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPago",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdPago",
                table: "Pedidos",
                column: "IdPago");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pago_IdPago",
                table: "Pedidos",
                column: "IdPago",
                principalTable: "Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pago_IdPago",
                table: "Pedidos");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdPago",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "IdPago",
                table: "Pedidos");
        }
    }
}
