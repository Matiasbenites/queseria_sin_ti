namespace QueseriaSoftware.Models.EstadosPedido
{
    public class NuevoState : IEstadoPedido
    {
        // Devuelve el nombre de este estado
        public string ObtenerEstado() => "Nuevo";

        // Cambia al siguiente estado lógico: En preparación
        public IEstadoPedido SiguienteEstado()
        {
            return new EnPreparacionState();
        }
    }
}
