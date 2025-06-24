namespace QueseriaSoftware.ViewModels
{
    public class FacturaViewModel
    {
        public int PedidoId { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public List<FacturaItemViewModel> Items { get; set; } = new();
        public decimal Total { get; set; }
        public string? MedioPago { get; set; }
    }

}
