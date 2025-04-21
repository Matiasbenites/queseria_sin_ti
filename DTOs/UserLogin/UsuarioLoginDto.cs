namespace QueseriaSoftware.DTOs.UserLogin
{
    public class UsuarioLoginDto
    {
        public int Id { get; set; }

        public required string NombreDeUsuario { get; set; }

        public required string Email { get; set; }

        public List<string>? Roles { get; set; }
    }
}
