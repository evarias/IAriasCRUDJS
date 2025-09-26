using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Products : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {

            return View();
        }
    }
}
