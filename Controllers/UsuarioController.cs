using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;
using System.Security.Claims;

namespace QueseriaSoftware.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuariosService _usuarioService;

        public UsuarioController(IUsuariosService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarDireccionAUsuario([FromBody] CrearDireccionDto model)
        {
            string usuarioId = User.Identity.IsAuthenticated
                ? User.FindFirst(ClaimTypes.NameIdentifier).Value
                : HttpContext.Session.Id;

            if (string.IsNullOrWhiteSpace(model.Calle) || string.IsNullOrWhiteSpace(model.TelefonoContacto) || !model.IdLocalidad.HasValue)
            {
                return BadRequest(new { success = false, message = "Datos incompletos." });
            }

            var resultado = await _usuarioService.AgregarDireccionUsuario(usuarioId, model.Calle, model.Numero, model.TelefonoContacto, model.IdLocalidad);

            if (!resultado.Success || resultado.Direccion == null)
            {
                return BadRequest(new { success = false, message = "No se pudo guardar la dirección." });
            }



            return Ok(new
            {
                success = true,
                message = "Dirección creada con éxito.",
                direccion = new
                {
                    id = resultado.Direccion.Id,
                    calle = resultado.Direccion.Calle,
                    numero = resultado.Direccion.Numero,
                    telefono = resultado.Direccion.TelefonoContacto,
                    localidad = resultado.Direccion.Localidad?.Nombre,
                    provincia = resultado.Direccion.Localidad?.Provincia?.Nombre
                }
            });
        }
    }
}
