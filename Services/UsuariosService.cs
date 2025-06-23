using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.ViewModels;

namespace QueseriaSoftware.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly AppDbContext _context;
        private readonly IDireccionService _direccionService;

        public UsuariosService(AppDbContext context, IDireccionService direccionService)
        {
            _context = context;
            _direccionService = direccionService;
        }

        public async Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId)
        {
            var idUsuario = int.Parse(usuarioId);
            var direcciones = await _context.Direcciones
                .Where(ud => ud.IdUsuario == idUsuario && ud.Activo && ud != null && ud.Activo)
                .Include(ud => ud.Localidad!)
                .ThenInclude(d => d.Provincia!)
                .Select(ud => ud!)
                .ToListAsync();

            var direccionesDto = direcciones.Select(d => new DireccionViewModel
            {
                Id = d.Id,
                Calle = d.Calle,
                Numero = d.Numero,
                TelefonoContacto = d.TelefonoContacto,
                Localidad = new LocalidadViewModel
                {
                    Nombre = d.Localidad!.Nombre,
                    Provincia = new ProvinciaViewModel
                    {
                        Nombre = d.Localidad.Provincia!.Nombre
                    }
                }
            }).ToList();

            return direccionesDto;
        }

        public async Task<ResultadoAgregarDireccionAUsuario> AgregarDireccionUsuario(string usuarioId, string calle, int? numero, string telefonoContacto, int? idLocalidad)
        {
            var resultado = new ResultadoAgregarDireccionAUsuario();

            if (!int.TryParse(usuarioId, out int idUsuario))
            {
                resultado.Success = false;
                resultado.Message = "El ID de usuario no es válido.";
                return resultado;
            }

            var resultadoCreacion = await _direccionService.CrearDireccion(idUsuario, calle, numero, telefonoContacto, idLocalidad);

            if (!resultadoCreacion.Success || resultadoCreacion.DireccionId == null)
            {
                resultado.Success = false;
                resultado.Message = "Error al crear dirección: " + resultadoCreacion.Message;
                return resultado;
            }
            resultado.Success = resultadoCreacion.Success;
            resultado.Direccion = new DireccionViewModel
            {
                Id = resultadoCreacion.DireccionId.Value,
                Calle = resultadoCreacion.Calle,
                Numero = resultadoCreacion.Numero ?? 0,
                TelefonoContacto = resultadoCreacion.TelefonoContacto,
                Localidad = new LocalidadViewModel { Nombre = resultadoCreacion.NombreLocalidad, Provincia = new ProvinciaViewModel { Nombre = resultadoCreacion.NombreProvincia} }
            };

            return resultado;
        }

        public async Task<Resultado> ValidarDireccion(string usuarioId, int direccionId)
        {
            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(x => x.Id == direccionId &&
                                          x.Activo &&
                                          x.IdUsuario == int.Parse(usuarioId));

            if (direccion != null)
            {
                return new Resultado { Success = true };
            }

            return new Resultado { Success = false, Message = "Dirección no válida" };
        }

    }
}
