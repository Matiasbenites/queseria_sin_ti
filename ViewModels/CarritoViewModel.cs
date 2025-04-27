namespace QueseriaSoftware.ViewModels
{
    public class CarritoViewModel
    {
        public int CarritoId { get; set; }
        public List<CarritoLineaViewModel> Lineas { get; set; } = new List<CarritoLineaViewModel>();
        public decimal Total { get; set; }
    }
}
