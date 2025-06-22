using Microsoft.AspNetCore.Mvc.Rendering;

namespace QueseriaSoftware.ViewModels
{
    public class SeleccionDireccionViewModel
    {
        public decimal TotalCompra { get; set; }

        public List<DireccionViewModel> Direcciones { get; set; } = new();

        public List<SelectListItem> Localidades { get; set; } = new();
    }

}
