using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.DTOs.Resultados.Pedido
{
    public class ResultadoCrearPedido
    {
        public decimal Total { get; set; }

        public List<DireccionViewModel>? Direcciones { get; set; }

        public bool PedidoPendienteDePago { get; set; }

        public bool PedidoConDireccionPendiente { get; set; }

        public bool PedidoSinProductos { get; set; }
    }
}
