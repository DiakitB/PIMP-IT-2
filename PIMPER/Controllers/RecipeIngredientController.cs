using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIMPER.DataP;
using PIMPER.Models;

namespace PIMPER.Controllers
{
    public class RecipeIngredientController : Controller
    {
        private readonly ApplicationContext _context;

        public RecipeIngredientController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: RecipeIngredient
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.recipeIngredientTables.Include(r => r.IngredientTable).Include(r => r.RecipeTable);
            return View(await applicationContext.ToListAsync());
        }

        // GET: RecipeIngredient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.recipeIngredientTables == null)
            {
                return NotFound();
            }

            var recipeIngredientTable = await _context.recipeIngredientTables
                .Include(r => r.IngredientTable)
                .Include(r => r.RecipeTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredientTable == null)
            {
                return NotFound();
            }

            return View(recipeIngredientTable);
        }

        // GET: RecipeIngredient/Create
        public IActionResult Create()
        {
            ViewData["IngredientTableId"] = new SelectList(_context.ingredientTables, "Id", "Name");
            ViewData["RecipeTableId"] = new SelectList(_context.recipesTable, "Id", "Name");
            return View();
        }

        // POST: RecipeIngredient/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IngredientTableId,RecipeTableId")] RecipeIngredientTable recipeIngredientTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeIngredientTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientTableId"] = new SelectList(_context.ingredientTables, "Id", "Name", recipeIngredientTable.IngredientTableId);
            ViewData["RecipeTableId"] = new SelectList(_context.recipesTable, "Id", "Name", recipeIngredientTable.RecipeTableId);
            return View(recipeIngredientTable);
        }

        // GET: RecipeIngredient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.recipeIngredientTables == null)
            {
                return NotFound();
            }

            var recipeIngredientTable = await _context.recipeIngredientTables.FindAsync(id);
            if (recipeIngredientTable == null)
            {
                return NotFound();
            }
            ViewData["IngredientTableId"] = new SelectList(_context.ingredientTables, "Id", "Name", recipeIngredientTable.IngredientTableId);
            ViewData["RecipeTableId"] = new SelectList(_context.recipesTable, "Id", "Name", recipeIngredientTable.RecipeTableId);
            return View(recipeIngredientTable);
        }

        // POST: RecipeIngredient/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IngredientTableId,RecipeTableId")] RecipeIngredientTable recipeIngredientTable)
        {
            if (id != recipeIngredientTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeIngredientTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeIngredientTableExists(recipeIngredientTable.Id))
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
            ViewData["IngredientTableId"] = new SelectList(_context.ingredientTables, "Id", "Name", recipeIngredientTable.IngredientTableId);
            ViewData["RecipeTableId"] = new SelectList(_context.recipesTable, "Id", "Name", recipeIngredientTable.RecipeTableId);
            return View(recipeIngredientTable);
        }

        // GET: RecipeIngredient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.recipeIngredientTables == null)
            {
                return NotFound();
            }

            var recipeIngredientTable = await _context.recipeIngredientTables
                .Include(r => r.IngredientTable)
                .Include(r => r.RecipeTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeIngredientTable == null)
            {
                return NotFound();
            }

            return View(recipeIngredientTable);
        }

        // POST: RecipeIngredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.recipeIngredientTables == null)
            {
                return Problem("Entity set 'ApplicationContext.recipeIngredientTables'  is null.");
            }
            var recipeIngredientTable = await _context.recipeIngredientTables.FindAsync(id);
            if (recipeIngredientTable != null)
            {
                _context.recipeIngredientTables.Remove(recipeIngredientTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeIngredientTableExists(int id)
        {
          return _context.recipeIngredientTables.Any(e => e.Id == id);
        }
    }
}
