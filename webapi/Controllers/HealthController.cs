using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        [Route("check")]
        public string Index()
        {
            return "service running OK";
        }
    }
}
