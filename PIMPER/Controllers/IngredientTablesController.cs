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
    public class IngredientTablesController : Controller
    {
        private readonly ApplicationContext _context;

        public IngredientTablesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: IngredientTables
        public  IActionResult Index()
        {
            var applicationContext = _context.ingredientTables.Include(i => i.QuantitiesTable).Include(i => i.UnitTable);
            return View( applicationContext);
        }

        // GET: IngredientTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ingredientTables == null)
            {
                return NotFound();
            }

            var ingredientTable = await _context.ingredientTables
                .Include(i => i.QuantitiesTable)
                .Include(i => i.UnitTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientTable == null)
            {
                return NotFound();
            }

            return View(ingredientTable);
        }

        // GET: IngredientTables/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> unitDropDown = _context.unitTables.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            IEnumerable<SelectListItem> quantitiesDropDown = _context.quantitiesTables.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            ViewBag.unitDropDown = unitDropDown;
            ViewBag.quantitiesDropDown = quantitiesDropDown;
            return View();
        }

        // POST: IngredientTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,UnitTableId,QuantitiesTableId")] IngredientTable ingredientTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredientTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuantitiesTableId"] = new SelectList(_context.quantitiesTables, "Id", "Id", ingredientTable.QuantitiesTableId);
            ViewData["UnitTableId"] = new SelectList(_context.unitTables, "Id", "Id", ingredientTable.UnitTableId);
            return View(ingredientTable);
        }

        // GET: IngredientTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ingredientTables == null)
            {
                return NotFound();
            }

            var ingredientTable = await _context.ingredientTables.FindAsync(id);
            if (ingredientTable == null)
            {
                return NotFound();
            }
            ViewData["QuantitiesTableId"] = new SelectList(_context.quantitiesTables, "Id", "Id", ingredientTable.QuantitiesTableId);
            ViewData["UnitTableId"] = new SelectList(_context.unitTables, "Id", "Id", ingredientTable.UnitTableId);
            return View(ingredientTable);
        }

        // POST: IngredientTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UnitTableId,QuantitiesTableId")] IngredientTable ingredientTable)
        {
            if (id != ingredientTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredientTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientTableExists(ingredientTable.Id))
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
            ViewData["QuantitiesTableId"] = new SelectList(_context.quantitiesTables, "Id", "Id", ingredientTable.QuantitiesTableId);
            ViewData["UnitTableId"] = new SelectList(_context.unitTables, "Id", "Id", ingredientTable.UnitTableId);
            return View(ingredientTable);
        }

        // GET: IngredientTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ingredientTables == null)
            {
                return NotFound();
            }

            var ingredientTable = await _context.ingredientTables
                .Include(i => i.QuantitiesTable)
                .Include(i => i.UnitTable)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientTable == null)
            {
                return NotFound();
            }

            return View(ingredientTable);
        }

        // POST: IngredientTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ingredientTables == null)
            {
                return Problem("Entity set 'ApplicationContext.ingredientTables'  is null.");
            }
            var ingredientTable = await _context.ingredientTables.FindAsync(id);
            if (ingredientTable != null)
            {
                _context.ingredientTables.Remove(ingredientTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientTableExists(int id)
        {
          return _context.ingredientTables.Any(e => e.Id == id);
        }
    }
}
