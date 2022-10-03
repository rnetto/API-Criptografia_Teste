using Microsoft.AspNetCore.Mvc;

namespace API_Criptografia01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Criptografia01 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
