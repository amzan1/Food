using Food.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Food.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ModelContext _context;
        

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Recipes()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult News() 
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            if(id == null)
            {
                id = HttpContext.Session.GetInt32("ChefId");
            }
            var user = _context.Users.Where(x => x.UserId == id).SingleOrDefault();

            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
