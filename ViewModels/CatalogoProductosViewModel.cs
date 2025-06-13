namespace QueseriaSoftware.ViewModels
{
    public class CatalogoProductosViewModel
    {
        public List<CarritoLineaViewModel> ProductosEnCarrito { get; set; } = new List<CarritoLineaViewModel>();
        public List<ProductoViewModel> Productos { get; set; } = new List<ProductoViewModel>();
    }
}
