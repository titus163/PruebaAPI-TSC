using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Prueba.Model;

namespace Prueba.Services
{
    /// <summary>
    /// JwtAuthHandler class to implemente authentication service with JWT
    /// </summary>
    public class JwtAuthHandler : IJwtAuthHandler
    {
        IConfiguration _config;

        public JwtAuthHandler(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// AuthenticateUser method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public JwtUserModel AuthenticateUser(JwtUserModel userInfo)
        {
            JwtUserModel login = null;

            //validate now
            if (userInfo.Username == _config["UserAuthenticationDetails:Username"] && userInfo.Password == _config["UserAuthenticationDetails:Password"])
            {
                login = new JwtUserModel
                {
                    Username = _config["UserAuthenticationDetails:Username"],
                    EmailAddress = _config["UserAuthenticationDetails:EmailAddress"],
                    Password = _config["UserAuthenticationDetails:Password"]
                };
            }
            return login;
        }

        /// <summary>
        /// GenerateJwtTokens method for generation of new tokens to access api methods
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string GenerateJwtTokens(JwtUserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //define claims...
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //define token and write
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: credentials
                );

            //fetch encoded Token
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedToken;
        }
    }
}
