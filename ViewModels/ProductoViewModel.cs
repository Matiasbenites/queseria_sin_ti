namespace QueseriaSoftware.ViewModels
{
    public class ProductoViewModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public required string ImagenUrl { get; set; }
        public int Stock { get; set; }
    }
}