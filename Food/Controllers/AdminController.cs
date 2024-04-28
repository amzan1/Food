using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult User()
        {
            return View();
        }

        public IActionResult Table()
        {
            return View();
        }
        public IActionResult Maps() 
        {
            return View();
        }

        public IActionResult Notifications()
        {
            return View();
        }
    }
}
