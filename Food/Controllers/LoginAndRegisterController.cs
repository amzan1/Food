using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login() 
        {
            return View();
        }
    }
}
