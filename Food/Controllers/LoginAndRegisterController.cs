using Food.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Food.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LoginAndRegisterController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string userName, string Password)
        {
            if (ModelState.IsValid)
            {

                user.ImagePath = "~/Images/default.webp";
                _context.Add(user);
                _context.SaveChanges();

                UserLogin userLogin = new UserLogin();
                userLogin.Username = userName;
                userLogin.Password = Password;
                userLogin.UserId = user.UserId;
                userLogin.RoleId = 3;



                _context.Add(userLogin);
                _context.SaveChanges();

                return RedirectToAction("Login" , "LoginAndRegister");
            }

            return View();
        }


        public IActionResult RegisterForChef()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterForChef(User user, string userName, string Password)
        {
            var userin = _context.UserLogins.Where(x=> x.Username == userName).SingleOrDefault();
            if (userin == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (user.ImageFile != null)
                        {
                            string wwwrootPath = _webHostEnvironment.WebRootPath;
                            string imageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                            string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);


                            using (var fileStream = new FileStream(fullPath, FileMode.Create))
                            {
                                await user.ImageFile.CopyToAsync(fileStream);
                            }
                            user.ImagePath = imageName;
                        }
                        else
                        {
                            user.ImagePath = "~/Images/default.webp";
                        }
                   
                        _context.Add(user);
                        await _context.SaveChangesAsync();

                        UserLogin userLogin = new UserLogin();
                        userLogin.Username = userName;
                        userLogin.Password = Password;
                        userLogin.UserId = user.UserId;
                        userLogin.RoleId = 2;

                        _context.Add(userLogin);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Login", "LoginAndRegister");
                    }
                    catch (Exception ex) { }
                }
            }
            ModelState.AddModelError("", "Username already exist");
            return View();
            
        }

        public IActionResult Login() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _context.UserLogins.
                Where(x => x.Username == userLogin.Username && x.Password == userLogin.Password).SingleOrDefaultAsync();

            if (user != null)
            {
                switch (user.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)user.UserId);
                        HttpContext.Session.SetString("AdminName", user.Username);
                        HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);


                        return RedirectToAction("Index","Admin");

                    case 2:
                        HttpContext.Session.SetInt32("ChefId", (int)user.UserId);
                        HttpContext.Session.SetString("UserName", user.Username);
                        HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);

                        return RedirectToAction("index", "Home");

                    case 3:
                        HttpContext.Session.SetInt32("UserId", (int)user.UserId);
                        HttpContext.Session.SetString("UserName", user.Username);
                        HttpContext.Session.SetInt32("RoleId", (int)user.RoleId);


                        return RedirectToAction("index", "Home");
                         
                }
            }

            ModelState.AddModelError("", "UserName or Password are incorret");
            return View();
        }

        public IActionResult Logout()

        {
            HttpContext.Session.Clear(); // it will clear the session at the end of request
            return RedirectToAction("Login", "LoginAndRegister");
        }
    }
}
