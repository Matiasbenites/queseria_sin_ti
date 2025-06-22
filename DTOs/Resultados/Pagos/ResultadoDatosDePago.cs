using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.DTOs.Resultados.Pagos
{
    public class ResultadoDatosDePago : ResultadoBase
    {
        public ConfirmarPedidoViewModel? ConfirmarPedidoViewModel { get; set; } = new ConfirmarPedidoViewModel();
    }
}
