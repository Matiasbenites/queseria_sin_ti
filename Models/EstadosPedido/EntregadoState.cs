namespace QueseriaSoftware.Models.EstadosPedido
{
    public class EntregadoState : IEstadoPedido
    {
        // Devuelve el nombre de este estado
        public string ObtenerEstado() => "Entregado";

        // Este es el estado final, no cambia a otro
        public IEstadoPedido SiguienteEstado()
        {
            return this; // Se queda en Entregado porque es estado final
        }
    }
}
