using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class IdPagoFkNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "IdPago",
                table: "Pedidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos",
                column: "IdPago",
                principalTable: "Pagos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "IdPago",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Pagos_IdPago",
                table: "Pedidos",
                column: "IdPago",
                principalTable: "Pagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
