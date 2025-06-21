namespace QueseriaSoftware.Models.EstadosPedido
{
    public class EnviadoState : IEstadoPedido
    {
        // Devuelve el nombre de este estado
        public string ObtenerEstado() => "Enviado";

        // Cambia al siguiente estado lógico: Entregado
        public IEstadoPedido SiguienteEstado()
        {
            return new EntregadoState();
        }
    }
}
