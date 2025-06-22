using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.DTOs.Resultados
{
    public class ResultadoAgregarDireccionAUsuario: ResultadoBase
    {
        public string? Calle { get; set; }
        public int? Numero { get; set; }
        public string? TelefonoContacto { get; set; }
        public int? IdLocalidad { get; set; }

        public DireccionViewModel? Direccion { get; set; }
    }
}
