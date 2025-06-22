namespace QueseriaSoftware.DTOs
{
    public class ProductoEnCarritoDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int CarritoLineaId { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
