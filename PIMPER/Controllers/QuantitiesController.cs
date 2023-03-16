using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMPER.DataP;
using PIMPER.Models;

namespace PIMPER.Controllers
{
	public class QuantitiesController : Controller
    {
        private readonly ApplicationContext _context;

        public QuantitiesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Quantities
        public async Task<IActionResult> Index()
        {
              return View(await _context.quantitiesTables.ToListAsync());
        }

        // GET: Quantities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.quantitiesTables == null)
            {
                return NotFound();
            }

            var quantitiesTable = await _context.quantitiesTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quantitiesTable == null)
            {
                return NotFound();
            }

            return View(quantitiesTable);
        }

        // GET: Quantities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quantities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] QuantitiesTable quantitiesTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quantitiesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quantitiesTable);
        }

        // GET: Quantities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.quantitiesTables == null)
            {
                return NotFound();
            }

            var quantitiesTable = await _context.quantitiesTables.FindAsync(id);
            if (quantitiesTable == null)
            {
                return NotFound();
            }
            return View(quantitiesTable);
        }

        // POST: Quantities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] QuantitiesTable quantitiesTable)
        {
            if (id != quantitiesTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quantitiesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuantitiesTableExists(quantitiesTable.Id))
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
            return View(quantitiesTable);
        }

        // GET: Quantities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.quantitiesTables == null)
            {
                return NotFound();
            }

            var quantitiesTable = await _context.quantitiesTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quantitiesTable == null)
            {
                return NotFound();
            }

            return View(quantitiesTable);
        }

        // POST: Quantities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.quantitiesTables == null)
            {
                return Problem("Entity set 'ApplicationContext.quantitiesTables'  is null.");
            }
            var quantitiesTable = await _context.quantitiesTables.FindAsync(id);
            if (quantitiesTable != null)
            {
                _context.quantitiesTables.Remove(quantitiesTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuantitiesTableExists(int id)
        {
          return _context.quantitiesTables.Any(e => e.Id == id);
        }
    }
}
