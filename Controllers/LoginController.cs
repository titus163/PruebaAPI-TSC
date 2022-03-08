using Microsoft.AspNetCore.Mvc;
using Prueba.Model;
using Prueba.Services;
using Microsoft.Extensions.Logging;

namespace Prueba.Controllers
{
    [Route("[controller]")]
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
