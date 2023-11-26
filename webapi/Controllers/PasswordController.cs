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

        public PasswordController(IConfiguration configuration)
        {
            _configuration = configuration;
            objPassword = new PasswordDataAccessLayer(_configuration);
        }

        [HttpGet]
        [Route("Index")]
        public IEnumerable<Password> Index()
        {
            return objPassword.GetAllPasswords();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create( Password Password)
        {
            await objPassword.AddPassword(Password);
           return Ok( );
        }

        [HttpGet]
        [Route("Details/{id}")]
        public Password Details(string id)
        {
            return objPassword.GetPasswordData(id);
        }

        [HttpPut]
        [Route("Edit")]
        public void Edit( Password Password)
        {
            objPassword.UpdatePassword(Password);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody]string id)
        {
            objPassword.DeletePassword(id);
            return Ok();
        }
    }
}
