using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Food.Models;

namespace Food.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ModelContext _context;

        public RecipesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
           
            var modelContext = _context.Recipes.Include(r => r.Category).Include(r => r.Chef);
            return View(await modelContext.ToListAsync());
        }
        public IActionResult AllRecipe()
        {
            var recipe = _context.Recipes.Include(r => r.Category).Include(r => r.Chef).ToList();

            return View(recipe);
        }
        [HttpPost]
        public IActionResult AllRecipe( DateTime? startDate, DateTime? endDate)
        {
            var recipe = _context.Recipes.Include(r => r.Category).Include(r => r.Chef).ToList();
            if(recipe == null)
            {
                return NotFound();
            }

            if (startDate == null && endDate == null)
            {
                return View(recipe);
            }
            else if(startDate != null && endDate == null)
            {
                recipe = recipe.Where(x=>x.RecipeDate.Value.Date>=startDate).ToList();
                return View(recipe);
            }
            else if(startDate == null && endDate != null)
            {
                recipe = recipe.Where(x=>x.RecipeDate.Value.Date<=endDate).ToList();
                return View(recipe);
            }

            recipe = recipe.Where(x=>x.RecipeDate.Value.Date>= startDate && x.RecipeDate.Value.Date <= endDate).ToList();

                return View(recipe);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Chef)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["ChefId"] = HttpContext.Session.GetInt32("ChefId");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,RecipepName,RecipeTime,RecipeDate,RecipeStatus,CategoryId,ChefId,Price,RecipeIngredients,RecipePreparation,Image1,Image2,Image3")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recipe.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Users, "UserId", "UserId", recipe.ChefId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recipe.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Users, "UserId", "UserId", recipe.ChefId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RecipeId,RecipepName,RecipeTime,RecipeDate,RecipeStatus,CategoryId,ChefId,Price,RecipeIngredients,RecipePreparation,Image1,Image2,Image3")] Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", recipe.CategoryId);
            ViewData["ChefId"] = new SelectList(_context.Users, "UserId", "UserId", recipe.ChefId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.Chef)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ModelContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(decimal id)
        {
          return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
