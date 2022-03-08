
/// <summary>
/// Modelo de datos para la autenticacion JWT
/// </summary>
namespace Prueba.Model
{
    /// <summary>
    /// Clase del modelo de datos de usuario para la autenticacion JWT
    /// </summary>
    public class JwtUserModel
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
