using Microsoft.AspNetCore.Mvc;
using Prueba.Model;
using Prueba.Services;
using Microsoft.Extensions.Logging;

namespace Prueba.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IJwtAuthHandler _jwtHandler;

        public LoginController(ILogger<LoginController> logger, IJwtAuthHandler jwtHandler)
        {
            _logger = logger;
            _jwtHandler = jwtHandler;
        }

        /// <summary>
        /// Login user method to ger JWT Tokens 
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Return JWT Token to get access to other controllers methods</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        [HttpPost]
        [Route("api/LoginUser")]
        public IActionResult Login([FromBody] JwtUserModel login)
        {
            IActionResult response = Unauthorized();
            var user = _jwtHandler.AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = _jwtHandler.GenerateJwtTokens(user);
                response = Ok(new { user, tokenString });
            }
            return response;
        }
    }    
}
