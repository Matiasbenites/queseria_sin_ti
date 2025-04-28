using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface ICatalogoService
    {
        Task<List<ProductoViewModel>> ConsultarCatalogo(string? busqueda = null);

        Task<int> ConsultarDisponibilidad(int productoId);

    }
}
