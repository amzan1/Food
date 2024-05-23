using Food.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Food.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProfileController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddRecipe()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecipe(Recipe recipe)
        {
            var chefid = HttpContext.Session.GetInt32("ChefId");
            if (ModelState.IsValid)
            {
                
                
                    if (recipe.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + recipe.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                           await recipe.ImageFile.CopyToAsync(fileStream);
                        }
                        recipe.Image1 = imageName;
                    }
                    else
                    {
                        return NotFound();
                    }

                    recipe.ChefId = chefid;
                    recipe.RecipeDate = DateTime.Now;
                    recipe.RecipeStatus = "pending";
                    _context.Add(recipe);
                    await _context.SaveChangesAsync();
                  

                TempData["SuccessMessage"] = "Your recipe has been successfully submitted!";
                return RedirectToAction("Success");

            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recipe.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Users, "UserId", "UserId", recipe.ChefId);
            ModelState.AddModelError("", "");

            return View(recipe);
        }

        public ActionResult Success()
        {
            return RedirectToAction("Profile", "Home");

        }

    }
}
