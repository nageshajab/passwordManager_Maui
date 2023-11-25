using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.DAL;
using webapi.Models;

namespace webapi.Services
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(LoginModel loginModel)
        {
            if (InvalidUser(loginModel))
                return BadRequest("Invalid username or password");

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

        private bool InvalidUser(LoginModel loginModel)
        {
            bool returnval = loginModel == null | string.IsNullOrEmpty(loginModel?.UserName) |
                string.IsNullOrEmpty(loginModel?.Password);
            if (returnval)
                return true;

            UserDataAccessLayer userDataAccessLayer = new(_config);
            User user = userDataAccessLayer.GetUserData(loginModel.UserName, loginModel.Password);
            if (user == null)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegistrationModel registrationModel)
        {
            if (IncorrectRegistrationModel(registrationModel))
                return BadRequest();

            UserDataAccessLayer userDataAccessLayer = new(_config);
            try
            {
                if (UsernameAlreadyexists(registrationModel.UserName))
                {
                    return BadRequest("Username already exists");
                }

                if (EmailAlreadyexists(registrationModel.Email))
                {
                    return BadRequest("Email already exists");
                }

                userDataAccessLayer.AddUser(new User()
                {
                    Email = registrationModel.Email,
                    Password1 = registrationModel.Password,
                    UserName = registrationModel.UserName,
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

        private bool UsernameAlreadyexists(string username)
        {
            UserDataAccessLayer userDataAccessLayer = new(_config);
            try
            {
                var user = userDataAccessLayer.GetUserByUserName(username);
                if (user != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool EmailAlreadyexists(string email)
        {
            UserDataAccessLayer userDataAccessLayer = new(_config);
            try
            {
                var user = userDataAccessLayer.GetUserByUserEmail(email);
                if (user != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
