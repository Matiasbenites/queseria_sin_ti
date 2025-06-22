using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.DTOs.Resultados.Direccion
{
    public class ResultadoCrearDireccion : ResultadoBase
    {
        public int? DireccionId { get; set; }
        public string? Calle { get; set; }
        public int? Numero { get; set; }
        public string? TelefonoContacto { get; set; }
        public int? IdLocalidad { get; set; }

        public string? NombreLocalidad { get; set; }

        public string? NombreProvincia { get; set; }
    }
}
