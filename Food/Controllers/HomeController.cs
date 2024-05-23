using Food.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe.Checkout;


namespace Food.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ModelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            var user = _context.Users.ToList();
            var userLogin = _context.UserLogins.ToList();

            var ChefInfo = from u in user
                           join ul in userLogin on u.UserId equals ul.UserId
                           select new JoinUsers { User = u, UserLogin = ul };

            var test = _context.Testimonials.Include(x=>x.User).ToList();
            var tuple = new Tuple<List<Category>,IEnumerable<JoinUsers>,List<Testimonial>>(category, ChefInfo, test);

            return View(tuple);
        }

        public IActionResult Recipes()
        {

            var recipe =  _context.Recipes.Where(x => x.RecipeStatus == "Accepted").ToList();

            return View(recipe);
        }
        [HttpPost]
        public async Task<IActionResult> Recipes(string search)
        {
            var recipe = await _context.Recipes.ToListAsync();
            if(!string.IsNullOrEmpty(search))
            {
                recipe = recipe.Where(x=>x.RecipepName.Contains(search)).ToList();
                return View(recipe);
            }
            return View(recipe);
        }
        public async Task<IActionResult> ChefRecipes(int id)
        {
            var chefRecipe = await _context.Recipes.Where(x => x.ChefId == id).ToListAsync();
            return View(chefRecipe);
        }

        public async Task<IActionResult> CategoryRecipes(int id)
        {
            var categoryRecipe = await _context.Recipes.Where(x => x.CategoryId == id).ToListAsync();
            return View(categoryRecipe);
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
        [HttpGet]
        public  IActionResult Profile ()
        {
            var Id = HttpContext.Session.GetInt32("UserId");
            if(Id == null)
            {
                Id = HttpContext.Session.GetInt32("ChefId");
            }
           

            var user1 =  _context.UserLogins.Include(x=>x.User).FirstOrDefault(x=>x.UserId == Id);
            if (user1 == null)
            {
                return NotFound();
            }
            //var Login = _context.UserLogins.FirstOrDefault(x => x.UserId == Id);
            //ViewBag.Username = Login?.Username;
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");

            return View(user1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Profile(UserLogin user1 )
        {
            var id = HttpContext.Session.GetInt32("UserId");
            if (id == null)
            {
                id = HttpContext.Session.GetInt32("ChefId");
            }
           



            try
            {
                var usernameToUpdate = _context.UserLogins.Include(x => x.User).FirstOrDefault(x => x.UserId == id);

                if ( usernameToUpdate == null)
                {
                    return NotFound();
                }
                var userImg = _context.Users.Where(x=>x.UserId ==  id).FirstOrDefault();
                user1.User.ImagePath = userImg.ImagePath;

                if(user1.User.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string imageName = Guid.NewGuid().ToString() + "_" + user1.User.ImageFile.FileName;
                    string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);


                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        user1.User.ImageFile.CopyTo(fileStream);
                    }
                    usernameToUpdate.User.ImagePath = imageName;
                }
                
                usernameToUpdate.Username = user1.Username;
                usernameToUpdate.User.FName = user1.User.FName;
                usernameToUpdate.User.Email = user1.User.Email;
                usernameToUpdate.User.LName = user1.User.LName;
                usernameToUpdate.Password = user1.Password;

            
                _context.Update(usernameToUpdate);
                
                _context.SaveChanges();
                // Redirect to the profile view
                TempData["SuccessMessage"] = "Your Update has been successfully!";
                return RedirectToAction("Success", "Profile");
            }
            catch (DbUpdateException ex) 
            {
                _logger.LogError(ex, "Error occurred while updating profile");

                throw;
            }
        }

        /*public async Task<IActionResult> Pay(decimal RId)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);


            var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == RId);
            //var chefRecipe =  _context.Recipes.Where(x => x.RecipeId == RId).FirstOrDefault();



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
                    UnitAmount = (long)(recipe.Price * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = recipe.RecipepName
                    }
                },
                Quantity = 1
            };
            options.LineItems.Add(sessionListItem);

            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }*/

        public async Task<IActionResult> CheckOut(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "LoginAndRegister");
            }
            var recipe = await _context.Recipes.Include(x=>x.Category).FirstOrDefaultAsync(x => x.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            ViewBag.vat15 = (recipe.Price * 15) / 100;

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(int id, string CardNumber,string CardHolderName, string ExpirationDate, string Cvv)
        {

           


            var recipe = await _context.Recipes.Include(x => x.Category).FirstOrDefaultAsync(x => x.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }
            var userId = HttpContext.Session.GetInt32("UserId");

            var user1= await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if(user1 == null)
            {
                return NotFound();
            }


           

            bool isValidVisa = await _context.Visas.AnyAsync(v => v.CardNumber == CardNumber &&  v.CardHolderName == CardHolderName && v.Cvv == Cvv);
            if (!isValidVisa)
            {
                ModelState.AddModelError("Card info", "Invalid Visa card information");
                return View(recipe);
            }
            Purchase purchase = new Purchase();
            purchase.UserId = userId;
            purchase.RecipeId = recipe.RecipeId;
            purchase.BayDate = DateTime.Now;
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            //Create PDF
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = recipe.Image1;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);

            string pdfFileName = "Invoice " + purchase.Id + " for recipe "+ recipe.RecipepName+ ".pdf";
            string pdfPath = Path.Combine(wwwRootPath + "/PDF/", pdfFileName);

            string pdfFileRecipe = recipe.RecipepName + ".pdf";
            string pdfPathRecipe = Path.Combine(wwwRootPath + "/PDF/", pdfFileRecipe);

            


            // start invoice pdf
            using (var memoryStream = new MemoryStream())
            {

                Document doc = new Document();
                if (!System.IO.File.Exists(pdfPath))
                {
                    
                    FileStream f = new FileStream(pdfPath, FileMode.Create);
                    PdfWriter writer =  PdfWriter.GetInstance(doc, f);
                    doc.Open();

                    doc.Add(new Paragraph($"Invoice NO# {purchase.Id}", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));
                    doc.Add(Chunk.NEWLINE);
                    doc.Add(Chunk.NEWLINE);
                    PdfPTable table = new PdfPTable(2);
                    table.HorizontalAlignment = Element.ALIGN_CENTER;
                    Image img = Image.GetInstance(path);
                    img.ScaleToFit(200, 200);
                    //start row 1
                    table.AddCell(img);
                    table.AddCell($"Recipe: {recipe.RecipepName} in the category: {recipe.Category.CategoryName}");
                    //end row 1
                    // start row 2
                    table.AddCell($"VAT 15% : {(recipe.Price * 15) / 100}");
                    table.AddCell($" Amount: {recipe.Price}");
                    // end row 2
                    //start row 3
                    PdfPCell cell = new PdfPCell(new Phrase($"Total : {recipe.Price + (recipe.Price * 15) / 100}"));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    doc.Add(table);
                    doc.Close();
                    writer.Close();


                }
                // end invoice pdf

                //start recipe pdf
                using (var memoryStreamForRecipe = new MemoryStream())
                {
                    Document document = new Document();
                    if (!System.IO.File.Exists(pdfPathRecipe))
                    {
                        FileStream ff = new FileStream(pdfPathRecipe, FileMode.Create);
                        PdfWriter writer1 = PdfWriter.GetInstance(document, ff);
                        document.Open();
                        document.Add(new Paragraph($"{recipe.RecipepName}", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));
                        Image recipeimg = Image.GetInstance(path);
                        recipeimg.ScaleToFit(200, 200);
                        document.Add(recipeimg);

                        PdfPTable recipeTable = new PdfPTable(2);
                        recipeTable.AddCell("Ingredients: ");
                        recipeTable.AddCell(recipe.RecipeIngredients);

                        recipeTable.AddCell("Preparation: ");
                        recipeTable.AddCell(recipe.RecipePreparation);

                        recipeTable.AddCell("Duration: ");
                        recipeTable.AddCell(recipe.RecipeTime);

                        document.Add(recipeTable);
                        document.Close();
                        writer1.Close() ;
                    }

                }


                SendEmail(user1.Email, pdfPath, pdfPathRecipe);
            }
           
            return RedirectToAction("Confirmation", new { id = purchase.Id });


        }

        public void SendEmail(string email, string InvoicePDF, string RecipePDF)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("amzan.1418@gmail.com", "ejgo xtpf ylwf fqjn")
            };
            
            Attachment attachment1 = new Attachment(InvoicePDF);
            Attachment attachment2 = new Attachment(RecipePDF);
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("amzan.1418@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Invoice and recipe";
            mail.Body = "Thank you for buy the recipe";
            mail.Attachments.Add(attachment1);
            mail.Attachments.Add(attachment2);

            client.Send(mail);

        }

       


    }
}

