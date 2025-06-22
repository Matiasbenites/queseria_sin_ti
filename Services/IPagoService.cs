using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.Resultados.Pagos;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public interface IPagoService
    {
        public Task<ResultadoDatosDePago> ObtenerDatosDePago(string usuarioId);

        Task<List<PagosViewModel>> ObtenerMediosPago();
    }
}
