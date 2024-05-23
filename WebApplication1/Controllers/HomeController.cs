using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _logger = logger;
            _context = context;          
            _webHostEnviroment = webHostEnviroment;

        }


    public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult contact()
        {
            return View();
        }
        public IActionResult Arabic_Recipes()
        {
            return View();
        }
        public IActionResult Recipes_Details(int recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);
            
                HttpContext.Session.SetInt32("RecipeId", (int)recipe.RecipeId);
                HttpContext.Session.SetString("RecipeName", recipe.RecipeName);
                HttpContext.Session.SetString("Ingredients", recipe.Ingredients);
                HttpContext.Session.SetInt32("Price", (int)recipe.Price);
            

            return RedirectToAction("Index"); 
        }



        public IActionResult Testimonial()
        {
            return View();
        }
        public IActionResult HomeChef()
        {
            return View();
        }
        public IActionResult AddRecipe()
        {
            return View();
        }
        public IActionResult HomeUser()
        {
            return View();
        }
        public IActionResult Recipe()
        {
            int categoryId = 1;
            var recipes = _context.Recipes.Where(r => r.CategoryId == categoryId).ToList();
            ViewBag.CategoryId = categoryId;
            return View(recipes);
        }
        public IActionResult RecipeItalian()
        {
            int categoryId = 2;
            var recipes = _context.Recipes.Where(r => r.CategoryId == categoryId).ToList();
            ViewBag.CategoryId = categoryId;
            return View(recipes);
        }
        public IActionResult RecipeAsian()
        {
            int categoryId = 3;
            var recipes = _context.Recipes.Where(r => r.CategoryId == categoryId).ToList();
            ViewBag.CategoryId = categoryId;
            return View(recipes);
        }
        public IActionResult RecipeSweet()
        {
            int categoryId = 4;
            var recipes = _context.Recipes.Where(r => r.CategoryId == categoryId).ToList();
            ViewBag.CategoryId = categoryId;
            return View(recipes);
        }
        public IActionResult Card(int recipeId)
        {
            var recipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId);

            HttpContext.Session.SetInt32("RecipeId", (int)recipe.RecipeId);
            HttpContext.Session.SetString("RecipeName", recipe.RecipeName);
            HttpContext.Session.SetString("Ingredients", recipe.Ingredients);
            HttpContext.Session.SetInt32("Price", (int)recipe.Price);


            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Card(int RecipeId, Visa visa, string email)
        {
            var card = await _context.Visas.SingleOrDefaultAsync(c => c.CardNumber == visa.CardNumber && c.ExpiryDate.Value.Date == visa.ExpiryDate.Value.Date && c.Cvc == visa.Cvc);
            var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.RecipeId == RecipeId);

            if (card == null || card.Balance < recipe.Price)
            {
                ViewBag.Message = "Invalid card details or insufficient balance";
            }
            card.Balance -= recipe.Price;
            _context.Visas.Update(card);
            await _context.SaveChangesAsync();

            string wwwRootPath = _webHostEnviroment.WebRootPath;
            string invoiceFileName = "Invoice_" + recipe.RecipeName + ".pdf";
            string invoicePath = Path.Combine(wwwRootPath, "PDF", invoiceFileName);
            CreateInvoicePdf(recipe, card, invoicePath);

            string recipeFileName = recipe.RecipeName + ".pdf";
            string recipePath = Path.Combine(wwwRootPath, "PDF", recipeFileName);
            CreateRecipePdf(recipe, recipePath);

            SendEmailWithAttachments(email, invoicePath, recipePath);

            ViewBag.Message = "Payment successful! Invoice and recipe have been sent to your email.";

            return View();

        }

        private void CreateInvoicePdf(Recipe recipe, Visa visa, string pdfPath)
        {

            using (var fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();
                document.Add(new Paragraph("Invoice for Recipe Purchase"));
                document.Add(new Paragraph("Recipe Name: " + recipe.RecipeName));
                document.Add(new Paragraph("Amount: $" + recipe.Price));
                document.Add(new Paragraph("Card Number: " + visa.CardNumber));
                document.Add(new Paragraph("Transaction Date: " + DateTime.Now.ToString("MM/dd/yyyy")));
                document.Close();
            }
        }

        public async void CreateRecipePdf(Recipe recipe)
        {
            var modelContext = _context.Recipes.Where(x => x.RecipeId == recipe.RecipeId).SingleOrDefaultAsync();
            string wwwRootPath = _webHostEnviroment.WebRootPath;
            string PdffileName = modelContext.Result.RecipeName + ".pdf";
            string Pdfpath = Path.Combine(wwwRootPath + "/PDF/", PdffileName);

            Document doc = new Document();
            if (!System.IO.File.Exists(Pdfpath))
            {
                FileStream fs = new FileStream(Pdfpath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                doc.Add(new Paragraph("Recipe Name: " + recipe.RecipeName));
                doc.Add(new Paragraph("Ingredients: " + recipe.Ingredients));
                doc.Add(new Paragraph("Instructions: " + recipe.Instructions));
                doc.Close();
            }
        }
        public void CreateRecipePdf(Recipe recipe, string pdfPath)
        {
            using (var fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();
                document.Add(new Paragraph("Recipe Name: " + recipe.RecipeName));
                document.Add(new Paragraph("Ingredients: " + recipe.Ingredients));
                document.Add(new Paragraph("Instructions: " + recipe.Instructions));
                document.Close();
            }
        }


        public void SendEmailWithAttachments(string email, string invoicePath, string recipePath)
        {
            using (MailMessage mail = new MailMessage())
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.ethereal.email")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("peyton.tillman@ethereal.email", "PSNpp5Ar2GbpNpnUfR"),
                    EnableSsl = true
                };

                mail.From = new MailAddress("peyton.tillman@ethereal.email");
                mail.To.Add(email);
                mail.Subject = "Your Recipe Purchase and Invoice";
                mail.Body = "Attached are your purchased recipe and invoice.";

                Attachment invoiceAttachment = new Attachment(invoicePath);
                mail.Attachments.Add(invoiceAttachment);

                Attachment recipeAttachment = new Attachment(recipePath);
                mail.Attachments.Add(recipeAttachment);

                SmtpServer.Send(mail);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
      
    
}
