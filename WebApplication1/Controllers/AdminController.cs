using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{

    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            //var userCount = _context.Users.Count();
            //var loginCount = _context.Logins.Count();
            //var recipeCount = _context.Recipes.Count();

            //// Pass counts to view
            //ViewBag.UserCount = userCount;
            //ViewBag.LoginCount = loginCount;
            //ViewBag.RecipeCount = recipeCount;
            int userCount = _context.Users.ToList().Count;
            ViewBag.UserCount = userCount;

            int loginCount = _context.Logins.ToList().Count;
            ViewBag.LoginCount = loginCount;

            int recipeCount = _context.Recipes.ToList().Count;
            ViewBag.RecipeCount = recipeCount;

            return View();
        }

        public IActionResult Table()
        {
            var users = _context.Users.ToList();

            // Store the users data in ViewData
            ViewData["Users"] = users;

            return View();
        }
      
        public IActionResult Billing()
        {
            return View();
        }
        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
       

    }
}
