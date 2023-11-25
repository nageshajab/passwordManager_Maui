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
        [Route("Password/Index")]
        public IEnumerable<Password> Index()
        {
            return objPassword.GetAllPasswords();
        }

        [HttpPost]
        [Route("Password/Create")]
        public void Create([FromBody] Password Password)
        {
            objPassword.AddPassword(Password);
        }

        [HttpGet]
        [Route("Password/Details/{id}")]
        public Password Details(string id)
        {
            return objPassword.GetPasswordData(id);
        }

        [HttpPut]
        [Route("Password/Edit")]
        public void Edit([FromBody] Password Password)
        {
            objPassword.UpdatePassword(Password);
        }

        [HttpDelete]
        [Route("Password/Delete/{id}")]
        public void Delete(string id)
        {
            objPassword.DeletePassword(id);
        }


    }
}
