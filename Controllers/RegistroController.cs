using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueseriaSoftware.Data;
using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;
using QueseriaSoftware.Services;
using QueseriaSoftware.ViewModels;
using System.Data;

namespace QueseriaSoftware.Controllers
{
    [AllowAnonymous]
    public class RegistroController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISesionService _sesionService;

        public RegistroController(AppDbContext context, ISesionService sesionService)
        {
            _context = context;
            _sesionService = sesionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new RegistroViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegistroViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificamos si el usuario ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email || u.NombreDeUsuario == model.NombreDeUsuario))
            {
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese email o nombre de usuario.");
                return View(model);
            }

            var passwordHashed = _sesionService.CalcularHash(model.Password);

            var usuario = new Usuario
            {
                Email = model.Email,
                NombreDeUsuario = model.NombreDeUsuario,
                Password = passwordHashed,
                Dni = model.Dni,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Telefono = model.Telefono
            };

            _context.Usuarios.Add(usuario);

            var rolCliente = await _context.Roles.FirstOrDefaultAsync(r => r.Id == 2);

            if (rolCliente != null)
            {
                var usuarioRol = new UsuarioRol
                {
                    Usuario = usuario,
                    IdRol = rolCliente.Id
                };

                _context.UsuarioRoles.Add(usuarioRol);
            }

            await _context.SaveChangesAsync();

            // Redirigir a vista de exito en el registro
            return RedirectToAction("RegistroExitoso");
        }

        [HttpGet]
        public IActionResult RegistroExitoso()
        {
            return View();
        }


    }
}
