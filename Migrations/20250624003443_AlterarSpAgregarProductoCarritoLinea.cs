using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueseriaSoftware.Migrations
{
    /// <inheritdoc />
    public partial class AlterarSpAgregarProductoCarritoLinea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SP: AgregarProductoCarritoLinea (crea carrito si no existe y guarda el precio del producto)
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE AgregarProductoCarritoLinea
                    @UsuarioId INT,
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @CarritoId INT;
                    DECLARE @Precio DECIMAL(18,2);

                    -- Obtener precio actual del producto
                    SELECT @Precio = Precio FROM Productos WHERE Id = @ProductoId;

                    -- Buscar carrito activo
                    SELECT TOP 1 @CarritoId = Id
                    FROM Carritos
                    WHERE IdUsuario = @UsuarioId AND Activo = 1;

                    -- Si no existe, crear uno nuevo
                    IF @CarritoId IS NULL
                    BEGIN
                        INSERT INTO Carritos (IdUsuario, CreadoEn, ModificadoEn, Activo)
                        VALUES (@UsuarioId, GETDATE(), GETDATE(), 1);

                        SET @CarritoId = SCOPE_IDENTITY();
                    END

                    -- Insertar o actualizar línea
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
                        INSERT INTO CarritoLineas (CarritoId, ProductoId, Cantidad, Precio)
                        VALUES (@CarritoId, @ProductoId, @Cantidad, @Precio);
                    END
                END
            ");

            // SP: ActualizarProductoCarritoLinea (usa carrito existente y actualiza línea, incluyendo precio)
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE ActualizarProductoCarritoLinea
                    @CarritoId INT,
                    @ProductoId INT,
                    @Cantidad INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    DECLARE @Precio DECIMAL(18,2);
                    SELECT @Precio = Precio FROM Productos WHERE Id = @ProductoId;

                    IF EXISTS (
                        SELECT 1 FROM CarritoLineas WHERE CarritoId = @CarritoId AND ProductoId = @ProductoId
                    )
                    BEGIN
                        UPDATE CarritoLineas
                        SET Cantidad = @Cantidad,
                            Precio = @Precio
                        WHERE CarritoId = @CarritoId AND ProductoId = @ProductoId;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO CarritoLineas (CarritoId, ProductoId, Cantidad, Precio)
                        VALUES (@CarritoId, @ProductoId, @Cantidad, @Precio);
                    END
                END
            ");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AgregarProductoCarritoLinea");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ActualizarProductoCarritoLinea");
        }

    }
}
