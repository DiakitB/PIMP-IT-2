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
    public class UnitController : Controller
    {
        private readonly ApplicationContext _context;

        public UnitController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Unit
        public async Task<IActionResult> Index()
        {
              return View(await _context.unitTables.ToListAsync());
        }

        // GET: Unit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.unitTables == null)
            {
                return NotFound();
            }

            var unitTable = await _context.unitTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitTable == null)
            {
                return NotFound();
            }

            return View(unitTable);
        }

        // GET: Unit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] UnitTable unitTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitTable);
        }

        // GET: Unit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.unitTables == null)
            {
                return NotFound();
            }

            var unitTable = await _context.unitTables.FindAsync(id);
            if (unitTable == null)
            {
                return NotFound();
            }
            return View(unitTable);
        }

        // POST: Unit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] UnitTable unitTable)
        {
            if (id != unitTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitTableExists(unitTable.Id))
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
            return View(unitTable);
        }

        // GET: Unit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.unitTables == null)
            {
                return NotFound();
            }

            var unitTable = await _context.unitTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitTable == null)
            {
                return NotFound();
            }

            return View(unitTable);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.unitTables == null)
            {
                return Problem("Entity set 'ApplicationContext.unitTables'  is null.");
            }
            var unitTable = await _context.unitTables.FindAsync(id);
            if (unitTable != null)
            {
                _context.unitTables.Remove(unitTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitTableExists(int id)
        {
          return _context.unitTables.Any(e => e.Id == id);
        }
    }
}
