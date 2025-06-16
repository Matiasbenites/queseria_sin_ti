using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IProductosService
    {
        Task<List<ProductoViewModel>> ConsultarCatalogo(string usuarioId);

        Task<bool> ConsultarDisponibilidad(int productoId, int cantidad);
    }
}
