using Food.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Food.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ModelContext _context;

        public PaymentController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //var id = HttpContext.Session.GetInt32("UserId");
            //var user = _context.Users.FirstOrDefault(x => x.UserId == id);


            var FindRecipe = await _context.Recipes.ToListAsync();
            if (FindRecipe == null)
            {
                return View();
            }
            return View(FindRecipe);
        }
        public async Task<IActionResult> First(int id)
        {
            var f = await _context.Recipes.ToListAsync();
            return View(f);
        }


        public IActionResult Pay(int RId) 
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);


            var recipe = _context.Recipes.FirstOrDefault(x => x.RecipeId == RId);
            if (recipe == null)
            {
                return NotFound();
            }
            var domain = "http://localhost:7039/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Payment/OrderConfirmation",
                CancelUrl = domain + "LoginAndRegister/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "Payment"
            };

            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(recipe.Price*100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = recipe.RecipepName
                    }
                },
                Quantity =1
            };
            options.LineItems.Add(sessionListItem);

            var service = new SessionService();
            Session session=service.Create(options);
            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }
    }
}
