namespace QueseriaSoftware.Models.EstadosPedido
{
    public class EnPreparacionState : IEstadoPedido
    {
        // Devuelve el nombre del estado actual
        public string ObtenerEstado() => "En preparación";

        // Retorna una nueva instancia del siguiente estado
        public IEstadoPedido SiguienteEstado()
        {
            return new EnviadoState();
        }
    }
}
