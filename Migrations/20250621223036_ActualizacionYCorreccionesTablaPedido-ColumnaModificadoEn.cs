using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionYCorreccionesTablaPedidoColumnaModificadoEn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pedidos");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificadoEn",
                table: "Pedidos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ModificadoEn",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioId",
                table: "Pedidos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
