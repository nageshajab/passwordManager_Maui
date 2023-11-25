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
    public class UserController : Controller
    {
        public IConfiguration _config;
        UserDataAccessLayer objUser;

        public UserController(IConfiguration config)
        {
            _config = config;
            objUser = new UserDataAccessLayer(_config);
        }

        [HttpGet]
        [Route("User/Index")]
        public IEnumerable<User> Index()
        {
            return objUser.GetAllUsers();
        }

        [HttpPost]
        [Route("User/Create")]
        public void Create([FromBody] User User)
        {
            objUser.AddUser(User);
        }

        [HttpGet]
        [Route("User/Details/{id}")] 
        public User Details(string id) 
        {
            return objUser.GetUserData(id); 
        }

        [HttpPut]
        [Route("User/Edit")] 
        public void Edit([FromBody] User User) 
        { 
            objUser.UpdateUser(User); 
        }

        [HttpDelete]
        [Route("User/Delete/{id}")] 
        public void Delete(string id) 
        { 
            objUser.DeleteUser(id); 
        }

       
    }
}
