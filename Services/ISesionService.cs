using QueseriaSoftware.DTOs.UserLogin;
using QueseriaSoftware.Models;

namespace QueseriaSoftware.Services
{
    /// <summary>
    /// Define las operaciones relacionadas con la gestión de sesiones de usuario.
    /// </summary>
    public interface ISesionService
    {
        /// <summary>
        /// Autentica un usuario a partir de su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario ingresado.</param>
        /// <param name="password">Contraseña ingresada.</param>
        /// <returns>Un objeto <see cref="UsuarioLoginDto"/> si la autenticación es exitosa; de lo contrario, null.</returns>
        Task<UsuarioLoginDto?> AutenticarAsync(string nombreUsuario, string password);

        /// <summary>
        /// Inicia la sesión para el usuario autenticado, almacenando sus datos en la sesión.
        /// </summary>
        /// <param name="usuario">El DTO del usuario autenticado.</param>
        Task IniciarSesionAsync(UsuarioLoginDto usuario);

        /// <summary>
        /// Cierra la sesión actual del usuario.
        /// </summary>
        Task CerrarSesionAsync();

        /// <summary>
        /// Calcula el hash de un texto plano, usado para comparar contraseñas.
        /// </summary>
        /// <param name="input">Texto de entrada a hashear.</param>
        /// <returns>Una cadena hash del texto proporcionado.</returns>
        string CalcularHash(string input);
    }
}

