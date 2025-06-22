using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QueseriaSoftware.Services
{
    public class UsuariosService : IUsuariosService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDireccionService _direccionService;

        public UsuariosService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IDireccionService direccionService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _direccionService = direccionService;
        }

        public async Task<bool> ValidarUsuario(int usuarioId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);

            return usuario != null;
        }


        public string ObtenerUsuarioId()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User.Identity?.IsAuthenticated == true)
            {
                return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            return context?.Session?.Id ?? throw new InvalidOperationException("No hay sesión disponible.");
        }

        public bool EsAutenticado()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        public async Task<List<DireccionViewModel>> ObtenerDireccionesDelUsuario(string usuarioId)
        {
            var idUsuario = int.Parse(usuarioId);
            var direcciones = await _context.UsuarioDirecciones
                .Where(ud => ud.IdUsuario == idUsuario && ud.Activo && ud.Direccion != null && ud.Direccion.Activo)
                .Include(ud => ud.Direccion!)
                    .ThenInclude(d => d.Localidad!)
                        .ThenInclude(l => l.Provincia)
                .Select(ud => ud.Direccion!)
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

        public async Task<ResultadoAgregarDireccionAUsuario> AgregarDireccionUsuario(
            string usuarioId, string calle, int? numero, string telefonoContacto, int? idLocalidad)
        {
            var resultado = new ResultadoAgregarDireccionAUsuario();

            var resultadoCreacion = await _direccionService.CrearDireccion(calle, numero, telefonoContacto, idLocalidad);
            if (!resultadoCreacion.Success || resultadoCreacion.DireccionId == null)
            {
                resultado.Success = false;
                resultado.Message = "Error al crear dirección: " + resultadoCreacion.Message;
                return resultado;
            }

            var resultadoAsociacion = await AsociarDireccionUsuario(usuarioId, resultadoCreacion.DireccionId.Value);
            if (!resultadoAsociacion.Success)
            {
                resultado.Success = false;
                resultado.Message = "Error al asociar dirección al usuario: " + resultadoAsociacion.Message;
                return resultado;
            }

            // Cargar datos en el resultado final
            resultado.Success = true;
            resultado.Message = "Dirección agregada y asociada exitosamente.";
            resultado.Calle = resultadoCreacion.Calle;
            resultado.Numero = resultadoCreacion.Numero;
            resultado.TelefonoContacto = resultadoCreacion.TelefonoContacto;
            resultado.IdLocalidad = resultadoCreacion.IdLocalidad;

            resultado.Direccion = new DireccionViewModel
            {
                Id = resultadoCreacion.DireccionId.Value,
                Calle = resultadoCreacion.Calle,
                Numero = resultadoCreacion.Numero ?? 0,
                TelefonoContacto = resultadoCreacion.TelefonoContacto,
                Localidad = new LocalidadViewModel { Nombre = resultadoCreacion.NombreLocalidad, Provincia = new ProvinciaViewModel { Nombre = resultadoCreacion.NombreProvincia } },
            };

            return resultado;
        }


        public async Task<Resultado> AsociarDireccionUsuario(string usuarioId, int? direccionId)
        {
            var resultado = new Resultado();

            if (!int.TryParse(usuarioId, out int idUsuario))
            {
                resultado.Success = false;
                resultado.Message = "El ID de usuario no es válido.";
                return resultado;
            }

            var existeRelacion = await _context.UsuarioDirecciones
                .AnyAsync(ud => ud.IdUsuario == idUsuario && ud.IdDireccion == direccionId);

            if (existeRelacion)
            {
                resultado.Success = false;
                resultado.Message = "La dirección ya está asociada al usuario.";
                return resultado;
            }

            var relacion = new UsuarioDireccion
            {
                IdUsuario = idUsuario,
                IdDireccion = direccionId.Value,
                Activo = true
            };

            _context.UsuarioDirecciones.Add(relacion);
            await _context.SaveChangesAsync();

            resultado.Success = true;
            resultado.Message = "Dirección asociada correctamente al usuario.";
            return resultado;
        }

    }
}
