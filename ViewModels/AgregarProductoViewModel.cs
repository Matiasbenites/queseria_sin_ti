namespace QueseriaSoftware.ViewModels
{
    public class AgregarProductoViewModel
    {
        public string? Busqueda { get; set; }
        public List<CarritoLineaViewModel> ProductosEnCarrito { get; set; } = new List<CarritoLineaViewModel>();
        public List<ProductoViewModel> Productos { get; set; } = new List<ProductoViewModel>();
    }
}
