namespace QueseriaSoftware.Models.EstadosPedido
{
    public class DireccionPendienteState : IEstadoPedido
    {
        public string ObtenerEstado() => "Direccion pendiente";

        // Cambia al siguiente estado lógico: Entregado
        public IEstadoPedido SiguienteEstado()
        {
            return new NuevoState();
        }
    }
}
