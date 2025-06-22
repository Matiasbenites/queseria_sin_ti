using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.Resultados.Direccion;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    public class DireccionService : IDireccionService
    {
        private readonly AppDbContext _context;

        public DireccionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResultadoCrearDireccion> CrearDireccion(string calle, int? numero, string telefonoContacto, int? idLocalidad)
        {
            var resultado = new ResultadoCrearDireccion();

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(calle))
            {
                resultado.Success = false;
                resultado.Message = "La calle es obligatoria.";
                return resultado;
            }

            if (string.IsNullOrWhiteSpace(telefonoContacto))
            {
                resultado.Success = false;
                resultado.Message = "El teléfono de contacto es obligatorio.";
                return resultado;
            }

            if (idLocalidad is null)
            {
                resultado.Success = false;
                resultado.Message = "La localidad es obligatoria.";
                return resultado;
            }

            var direccion = new Direccion
            {
                Calle = calle,
                Numero = numero ?? 0,
                TelefonoContacto = telefonoContacto,
                IdLocalidad = idLocalidad.Value,
                Activo = true
            };

            _context.Direcciones.Add(direccion);
            await _context.SaveChangesAsync();

            var localidad = await _context.Localidades
                .Include(x => x.Provincia)
                .FirstOrDefaultAsync(x => x.Id == direccion.IdLocalidad);

            resultado.Success = true;
            resultado.Message = "Dirección creada con éxito.";
            resultado.Calle = direccion.Calle;
            resultado.Numero = direccion.Numero;
            resultado.TelefonoContacto = direccion.TelefonoContacto;
            resultado.IdLocalidad = localidad.Id;
            resultado.DireccionId = direccion.Id;
            resultado.NombreLocalidad = localidad.Nombre;
            resultado.NombreProvincia = localidad.Provincia.Nombre;

            return resultado;
        }
    }
}
