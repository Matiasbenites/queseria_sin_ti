using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class AlterarSpObtenerCarrito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SP: AgregarProductoCarritoLinea (crea carrito si no existe)
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE AgregarProductoCarritoLinea
                    @UsuarioId INT,
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @CarritoId INT;

                    SELECT TOP 1 @CarritoId = Id
                    FROM Carritos
                    WHERE IdUsuario = @UsuarioId AND Activo = 1;

                    IF @CarritoId IS NULL
                    BEGIN
                        INSERT INTO Carritos (IdUsuario, CreadoEn, ModificadoEn, Activo)
                        VALUES (@UsuarioId, GETDATE(), GETDATE(), 1);

                        SET @CarritoId = SCOPE_IDENTITY();
                    END

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

            // SP: ActualizarProductoCarritoLinea (requiere CarritoId)
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE ActualizarProductoCarritoLinea
                    @CarritoId INT,
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    SET NOCOUNT ON;

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

    }
}
