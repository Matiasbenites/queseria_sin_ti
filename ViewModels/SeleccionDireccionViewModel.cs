using Microsoft.AspNetCore.Mvc.Rendering;

namespace QueseriaSoftware.ViewModels
{
    public class SeleccionDireccionViewModel
    {
        public decimal TotalCompra { get; set; }

        public List<DireccionViewModel> Direcciones { get; set; } = new();
        public List<SelectListItem> Localidades { get; set; } = new();

        // Campos que vienen del <form>
        public int? DireccionSeleccionada { get; set; }

        public string? Calle { get; set; }
        public int? Numero { get; set; }
        public string? TelefonoContacto { get; set; }
        public int? IdLocalidad { get; set; }
    }


}
