using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [Display(Name = "Correo")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
