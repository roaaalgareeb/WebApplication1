using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Runtime.Intrinsics.X86;

namespace WebApplication1.Controllers
{
    public class RegisterAndLoginController : Controller
    {
        private readonly ModelContext _context;

        public RegisterAndLoginController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
  
        public IActionResult Register(User user, string Email, string password)
        {
            var users =  _context.Logins.Where(x => x.Email == Email).SingleOrDefault();

            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();

                Login login = new Login();
                login.Email = Email;
                login.Password = password;
                login.RoleId = user.RoleId;
                login.UserId = user.UserId;
                _context.Add(login);
                _context.SaveChanges();

                return RedirectToAction("Login", "RegisterAndLogin");
            }
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login,int RoleId)
        {
            if (ModelState.IsValid)
            {
                var auth = await _context.Logins.FirstOrDefaultAsync(x => x.Email == login.Email && x.Password == login.Password);

                if (auth != null)
                {
                    switch (auth.RoleId)
                    {
                        case 1:
                            HttpContext.Session.SetInt32("AdminID", (int)auth.UserId);
                            return RedirectToAction("Index", "Admin");
                        case 2:
                            HttpContext.Session.SetInt32("UserId", (int)auth.UserId);
                            return RedirectToAction("HomeUser", "Home");
                        case 3:
                            HttpContext.Session.SetInt32("ChefId", (int)auth.UserId);
                            return RedirectToAction("HomeChef", "Home");
                        default:
                            ModelState.AddModelError("", "Invalid role.");
                            break;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(Login login, int RoleId)
        //{
        //    var auth = await _context.Logins.Where(x => x.Email == login.Email && x.Password == login.Password).SingleOrDefaultAsync();
        //    if (auth != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            switch (auth.RoleId)
        //            {
        //                case 1:
        //                    HttpContext.Session.SetInt32("AdminID",(int)auth.UserId);
        //                    return RedirectToAction("Index", "Admin");
        //                case 2:
        //                    HttpContext.Session.SetInt32("UserId", (int)auth.UserId);
        //                    return RedirectToAction("HomeUser", "Home");
        //                case 3:
        //                    HttpContext.Session.SetInt32("ChefId", (int)auth.UserId);
        //                    return RedirectToAction("HomeChef", "Home");

        //            }
        //        }
        //        return View();
        //    }
        //    return View();

        //}
    }
}
    