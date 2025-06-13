using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IProductosService
    {
        Task<List<ProductoViewModel>> ConsultarCatalogo();

        Task<int> ConsultarDisponibilidad(int productoId);
    }
}
