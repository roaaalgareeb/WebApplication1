using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RecipeUsersController : Controller
    {
        private readonly ModelContext _context;

        public RecipeUsersController(ModelContext context)
        {
            _context = context;
        }

        // GET: RecipeUsers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.RecipeUsers.Include(r => r.EmailNavigation).Include(r => r.Recipe);
            return View(await modelContext.ToListAsync());
        }

        // GET: RecipeUsers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.RecipeUsers == null)
            {
                return NotFound();
            }

            var recipeUser = await _context.RecipeUsers
                .Include(r => r.EmailNavigation)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipeUser == null)
            {
                return NotFound();
            }

            return View(recipeUser);
        }

        // GET: RecipeUsers/Create
        public IActionResult Create()
        {
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId");
            return View();
        }

        // POST: RecipeUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeId,Email,PurchaseDate")] RecipeUser recipeUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", recipeUser.Email);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", recipeUser.RecipeId);
            return View(recipeUser);
        }

        // GET: RecipeUsers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.RecipeUsers == null)
            {
                return NotFound();
            }

            var recipeUser = await _context.RecipeUsers.FindAsync(id);
            if (recipeUser == null)
            {
                return NotFound();
            }
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", recipeUser.Email);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", recipeUser.RecipeId);
            return View(recipeUser);
        }

        // POST: RecipeUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RecipeId,Email,PurchaseDate")] RecipeUser recipeUser)
        {
            if (id != recipeUser.RecipeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeUserExists(recipeUser.RecipeId))
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
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email", recipeUser.Email);
            ViewData["RecipeId"] = new SelectList(_context.Recipes, "RecipeId", "RecipeId", recipeUser.RecipeId);
            return View(recipeUser);
        }

        // GET: RecipeUsers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.RecipeUsers == null)
            {
                return NotFound();
            }

            var recipeUser = await _context.RecipeUsers
                .Include(r => r.EmailNavigation)
                .Include(r => r.Recipe)
                .FirstOrDefaultAsync(m => m.RecipeId == id);
            if (recipeUser == null)
            {
                return NotFound();
            }

            return View(recipeUser);
        }

        // POST: RecipeUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.RecipeUsers == null)
            {
                return Problem("Entity set 'ModelContext.RecipeUsers'  is null.");
            }
            var recipeUser = await _context.RecipeUsers.FindAsync(id);
            if (recipeUser != null)
            {
                _context.RecipeUsers.Remove(recipeUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeUserExists(decimal id)
        {
          return (_context.RecipeUsers?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
