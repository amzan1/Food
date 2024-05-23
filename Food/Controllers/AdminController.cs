using Food.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace Food.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;


        public AdminController (ModelContext context, IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        public IActionResult Index()
        {
            //var id = HttpContext.Session.GetInt32("AdminId");
            //var user = _context.Users.Where(x => x.UserId == id).SingleOrDefault();

            ViewBag.NumberOfUsers = _context.UserLogins.Count(x => x.RoleId == 3);  
            ViewBag.NumberOfChefs = _context.UserLogins.Count(x => x.RoleId == 2);
            ViewBag.NumberOfRecipe = _context.Recipes.Count();
            ViewBag.NumberOfPurchases = _context.Purchases.Count(); 

            

            var user = _context.Users.ToList();
            var userLogin = _context.UserLogins.ToList();

            var UserInfo = from u in user
                           join ul in userLogin on u.UserId equals ul.UserId
                           select new JoinUsers { User = u, UserLogin = ul};

            return View(UserInfo);
        }

        public IActionResult UpdateStatus(decimal id, string status)
        {
            var recipe = _context.Recipes.Find(id);
            if (recipe != null)
            {
                recipe.RecipeStatus = status;
                _context.SaveChanges();
            }
            return RedirectToAction("index", "Recipes");
        }

        public IActionResult TestimonialStatus(decimal id, string status)
        {
            var t = _context.Testimonials.Find(id);
            if (t != null)
            {
                t.TestimonialStatus = status;
                _context.SaveChanges();
            }
            
            return RedirectToAction("RequestTest", "Testimonials");
        }
        public IActionResult AdminProfile()
        {
            var Id = HttpContext.Session.GetInt32("AdminId");



            var user1 = _context.UserLogins.FirstOrDefault(x => x.UserId == Id);
            if (user1 == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");


            return View(user1);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminProfile(UserLogin user1)
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            
            try
            {
                var usernameToUpdate = _context.UserLogins.FirstOrDefault(x => x.UserId == id);

                if (usernameToUpdate == null)
                {
                    return NotFound();
                }
                

                usernameToUpdate.Username = user1.Username;
                
                usernameToUpdate.Password = user1.Password;


                _context.Update(usernameToUpdate);

                _context.SaveChanges();
                // Redirect to the profile view
                TempData["SuccessMessage"] = "Your Update profile has been successfully!";
                return RedirectToAction("Success", "Admin");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred while updating profile");

                throw;
            }
        }
        public ActionResult Success()
        {
            return RedirectToAction("Index", "Admin");

        }
        public IActionResult Table()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Reports() 
        {
            var userlogin = _context.UserLogins.ToList();
            var category = _context.Categories.ToList();
            var recipe = _context.Recipes.ToList();
            var purchase = _context.Purchases.ToList();


            var AllPurchase = from u in userlogin
                              join p in purchase on u.UserId equals p.UserId
                              join r in recipe on p.RecipeId equals r.RecipeId
                              join c in category on r.CategoryId equals c.Id
                              select new JoinPurchases { UserLogin = u, Recipe = r, Category = c, Purchase = p };
            var result = _context.Purchases.Include(x => x.User).Include(x => x.Recipe).ToList();
            ViewBag.NumberOfPurchases = result.Count();
            //ViewBag.totaPrice = result.Sum(x => x.Recipe.Price);

            var tuple = Tuple.Create<IEnumerable<JoinPurchases>, IEnumerable<Purchase>>(AllPurchase, result);
            return View(tuple);
        }

        [HttpPost]
        public async Task<IActionResult> Reports(DateTime? startDate, DateTime? endDate)
        {

            var userlogin = _context.UserLogins.ToList();
            var category = _context.Categories.ToList();
            var recipe = _context.Recipes.ToList();
            var purchase = _context.Purchases.ToList();


            var AllPurchase = from u in userlogin
                              join p in purchase on u.UserId equals p.UserId
                              join r in recipe on p.RecipeId equals r.RecipeId
                              join c in category on r.CategoryId equals c.Id
                              select new JoinPurchases { UserLogin = u, Recipe = r, Category = c, Purchase = p };
            var result = _context.Purchases.Include(x => x.User).Include(x => x.Recipe).ToList();
            if (startDate == null && endDate == null)
            {
                ViewBag.NumberOfPurchases = result.Count();
                ViewBag.totaPrice = result.Sum(x => x.Recipe.Price);

                var tuple = Tuple.Create<IEnumerable<JoinPurchases>, IEnumerable<Purchase>>(AllPurchase, result);
                return View(tuple);
            }
            else if (startDate == null && endDate != null)
            {
                ViewBag.NumberOfPurchases = result.Where(x => x.BayDate.Value.Date == endDate).Count();
                ViewBag.totaPrice = result.Where(x => x.BayDate.Value.Date == endDate).Sum(x => x.Recipe.Price);

                var tuple = Tuple.Create<IEnumerable<JoinPurchases>, IEnumerable<Purchase>>(AllPurchase, result.Where(x => x.BayDate.Value.Date == endDate).ToList());
                return View(tuple);
            }
            else if (startDate != null && endDate == null)
            {
                ViewBag.NumberOfPurchases = result.Where(x => x.BayDate.Value.Date == startDate).Count();
                ViewBag.totaPrice = result.Where(x => x.BayDate.Value.Date == startDate).Sum(x => x.Recipe.Price);

                var tuple = Tuple.Create<IEnumerable<JoinPurchases>, IEnumerable<Purchase>>(AllPurchase, result.Where(x => x.BayDate.Value.Date == startDate).ToList());
                return View(tuple);
            }
            else
            {
                ViewBag.NumberOfPurchases = result.Where(x => x.BayDate >= startDate && x.BayDate <=endDate).Count();
               // ViewBag.totaPrice = result.Where(x => x.BayDate >= startDate && x.BayDate <= endDate).Sum(x => x.Recipe.Price);

                var tuple = Tuple.Create<IEnumerable<JoinPurchases>, IEnumerable<Purchase>>(AllPurchase, result.Where(x => x.BayDate >= startDate && x.BayDate <= endDate).ToList());
                return View(tuple);
            }
        }

        public IActionResult Purchase() 
        {
            var userlogin = _context.UserLogins.ToList();
            var category = _context.Categories.ToList();
            var recipe = _context.Recipes.ToList();
            var purchase = _context.Purchases.ToList();


            var AllPurchase = from u in userlogin
                              join p in purchase on u.UserId equals p.UserId
                              join r in recipe on p.RecipeId equals r.RecipeId
                              join c in category on r.CategoryId equals c.Id
                              select new JoinPurchases { UserLogin = u, Recipe = r, Category = c, Purchase = p };
                           

            return View(AllPurchase);
        }

        

        
        public IActionResult Notifications()
        {
           
            return View();
        }
    }
}
