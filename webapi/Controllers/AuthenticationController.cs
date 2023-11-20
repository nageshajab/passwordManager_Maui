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
using webapi.DAL;
using webapi.Models;

namespace webapi.Services
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        public string BaseAddress = "";

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(LoginModel loginModel)
        {
            UserDataAccessLayer userDataAccessLayer = new UserDataAccessLayer();
            User user = userDataAccessLayer.GetUserData(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                return NotFound();
            }

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
            return Ok(tokenHandler.WriteToken(token));
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            bool isSuccess;
            string errorMessage;

            UserDataAccessLayer userDataAccessLayer = new UserDataAccessLayer();
            try
            {
                userDataAccessLayer.AddUser(new User()
                {
                    UserName = registrationModel.Email,
                    Email = registrationModel.Email,
                    Password1 = registrationModel.Password,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);    
            }

            return StatusCode(StatusCodes.Status201Created, "");
        }
    }
}
