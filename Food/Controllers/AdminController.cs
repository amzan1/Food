using Food.Models;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        public AdminController (ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
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
