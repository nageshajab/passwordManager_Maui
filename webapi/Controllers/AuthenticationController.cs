using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<string> AuthenticateUser(LoginModel loginModel)
        {
            string returnStr = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyDetail = Encoding.UTF8.GetBytes("this is my custom Secret key for authentication");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"userid1"),
                new Claim(JwtRegisteredClaimNames.Name,$"{loginModel.UserName}")
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "jwt_audience",
                Issuer = "jwt_issuer",
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return OK(tokenHandler.WriteToken(token));
            return tokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            if (IncorrectRegistrationModel(registrationModel) )
                return BadRequest();

            bool isSuccess;
            string errorMessage;

            AuthenticationDBContext dbContext = new();
            try
            {
                dbContext.UserRecord.InsertOne(new User()
                {
                    Email = registrationModel.Email,
                    Password1 = registrationModel.Password,
                    UserName = registrationModel.Email,
                    FirstName = registrationModel.FirstName,
                    LastName = registrationModel.LastName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message + ex.InnerException?.Message);
            }

            return Ok();
        }

        private bool IncorrectRegistrationModel(RegistrationModel registrationModel)
        {
            bool returnval = registrationModel == null | string.IsNullOrEmpty(registrationModel?.Email) |
                string.IsNullOrEmpty(registrationModel?.Gender) |
                string.IsNullOrEmpty(registrationModel?.Password) |
                string.IsNullOrEmpty(registrationModel?.LastName) |
                string.IsNullOrEmpty(registrationModel?.FirstName);

            return returnval;
        }
    }
}
