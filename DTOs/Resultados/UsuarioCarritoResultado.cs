using QueseriaSoftware.Models;

namespace QueseriaSoftware.DTOs.Resultados
{
    public class UsuarioCarritoResultado : ResultadoBase
    {
        public Carrito? Carrito { get; set; }
    }
}
