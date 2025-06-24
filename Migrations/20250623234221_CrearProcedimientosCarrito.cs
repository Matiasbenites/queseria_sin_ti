using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class CrearProcedimientosCarrito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ConsultarDisponibilidad
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE ConsultarDisponibilidad
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM Productos WHERE Id = @ProductoId AND Stock >= @Cantidad
                    )
                        SELECT 1 AS Disponible;
                    ELSE
                        SELECT 0 AS Disponible;
                END
            ");

            // ObtenerCarrito
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE ObtenerCarrito
                    @UsuarioId INT
                AS
                BEGIN
                    SELECT TOP 1 *
                    FROM Carritos
                    WHERE IdUsuario = @UsuarioId AND Activo = 1;
                END
            ");

            // AgregarProductoCarritoLinea
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE AgregarProductoCarritoLinea
                    @CarritoId INT,
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM CarritoLineas WHERE CarritoId = @CarritoId AND ProductoId = @ProductoId
                    )
                    BEGIN
                        UPDATE CarritoLineas
                        SET Cantidad = @Cantidad
                        WHERE CarritoId = @CarritoId AND ProductoId = @ProductoId;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO CarritoLineas (CarritoId, ProductoId, Cantidad)
                        VALUES (@CarritoId, @ProductoId, @Cantidad);
                    END
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ConsultarDisponibilidad");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ObtenerCarrito");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AgregarProductoCarritoLinea");
        }

    }
}
