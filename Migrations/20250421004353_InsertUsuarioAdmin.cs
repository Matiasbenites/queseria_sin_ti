using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class InsertUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Activo", "CreadoEn", "Descripcion", "ModificadoEn", "Nombre" },
                values: new object[] { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rol de administrador", null, "Admin" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "Apellido", "CreadoEn", "Dni", "Email", "ModificadoEn", "Nombre", "NombreDeUsuario", "Password", "Telefono" },
                values: new object[] { 1, true, "General", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "12345678", "admin@queseria.com", null, "Administrador", "admin", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", "123456789" });

            migrationBuilder.InsertData(
                table: "UsuarioRoles",
                columns: new[] { "Id", "Activo", "IdRol", "IdUsuario" },
                values: new object[] { 1, true, 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuarioRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
