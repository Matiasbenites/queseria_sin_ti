namespace QueseriaSoftware.ViewModels
{
    public class AgregarProductoViewModel
    {
        public string? Busqueda { get; set; }
        public List<ProductoViewModel> Productos { get; set; } = new List<ProductoViewModel>();
    }
}
