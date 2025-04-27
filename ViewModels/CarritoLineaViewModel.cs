namespace QueseriaSoftware.ViewModels
{
    public class CarritoLineaViewModel
    {
        public int LineaId { get; set; }
        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
