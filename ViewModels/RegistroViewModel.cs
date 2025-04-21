using System.ComponentModel.DataAnnotations;

namespace QueseriaSoftware.ViewModels
{
    public class RegistroViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string? NombreDeUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string? Password { get; set; }

        [Required]
        [Display(Name = "DNI")]
        public string? Dni { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Apellido { get; set; }

        [Phone]
        public string? Telefono { get; set; }
    }
}
