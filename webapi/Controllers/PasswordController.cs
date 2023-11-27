using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.DAL;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : Controller
    {
        private IConfiguration _configuration;
        PasswordDataAccessLayer objPassword;
        UserDataAccessLayer UserDataAccessLayer;

        public PasswordController(IConfiguration configuration)
        {
            _configuration = configuration;
            objPassword = new PasswordDataAccessLayer(_configuration);
            UserDataAccessLayer = new UserDataAccessLayer(_configuration);
        }

        [HttpPost]
        [Route("Index")]
        public async Task<IActionResult> Index([FromBody] string userid)
        {
            if (!UserDataAccessLayer.UserExists(userid))
                return BadRequest("invalid userid");

            return Ok(objPassword.GetAllPasswords(userid));
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Password Password)
        {
            if (!UserDataAccessLayer.UserExists(Password.UserId))
                return BadRequest("invalid userid");

            await objPassword.AddPassword(Password);
            return Ok();
        }

        [HttpPost]
        [Route("Details")]
        public async Task<IActionResult> Details(Password password)
        {
            if (!UserDataAccessLayer.UserExists(password.UserId))
                return BadRequest("invalid userid");

            if (string.IsNullOrEmpty(password.Id))
                return BadRequest("Id cannot be null");


            return Ok(objPassword.GetPasswordData(password));
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Password password)
        {

            if (!UserDataAccessLayer.UserExists(password.UserId))
                return BadRequest("invalid userid");

            if (string.IsNullOrEmpty(password.Id))
                return BadRequest("Id cannot be null");

            objPassword.UpdatePassword(password);
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Password password)
        {

            if (!UserDataAccessLayer.UserExists(password.UserId))
                return BadRequest("invalid userid");

            objPassword.DeletePassword(password.Id);
            return Ok();
        }
    }
}
