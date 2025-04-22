using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email incorrecto")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [Display(Name = "Nombre de Usuario")]
        public string? NombreDeUsuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [Display(Name = "DNI")]
        public string? Dni { get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string? Apellido { get; set; }

        [Phone]
        public string? Telefono { get; set; }
    }
}
