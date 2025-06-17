namespace QueseriaSoftware.Models.EstadosPedido
{
    public interface IEstadoPedido
    {
        string ObtenerEstado();              // Devuelve el nombre del estado actual
        IEstadoPedido SiguienteEstado();     // Retorna el siguiente estado (según la lógica)
    }
}
