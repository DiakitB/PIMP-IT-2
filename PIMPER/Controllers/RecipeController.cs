using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMPER.DataP;
using PIMPER.Models;

namespace PIMPER.Controllers
{
	public class RecipeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public RecipeController(ApplicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Recipe
        public async Task<IActionResult> Index()
        {
              return View(await _context.recipesTable.ToListAsync());
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.recipesTable == null)
            {
                return NotFound();
            }

            var recipeTable = await _context.recipesTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeTable == null)
            {
                return NotFound();
            }

            return View(recipeTable);
        }

        // GET: Recipe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RecipeTable obj, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (obj.Id == 0)
                {
                    _context.recipesTable.Add(obj);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            return View(obj);
        }

        // GET: Recipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.recipesTable == null)
            {
                return NotFound();
            }

            var recipeTable = await _context.recipesTable.FindAsync(id);
            if (recipeTable == null)
            {
                return NotFound();
            }
            return View(recipeTable);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl")] RecipeTable recipeTable)
        {
            if (id != recipeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeTableExists(recipeTable.Id))
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
            return View(recipeTable);
        }

        // GET: Recipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.recipesTable == null)
            {
                return NotFound();
            }

            var recipeTable = await _context.recipesTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipeTable == null)
            {
                return NotFound();
            }

            return View(recipeTable);
        }

        // POST: Recipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.recipesTable == null)
            {
                return Problem("Entity set 'ApplicationContext.recipesTable'  is null.");
            }
            var recipeTable = await _context.recipesTable.FindAsync(id);
            if (recipeTable != null)
            {
                _context.recipesTable.Remove(recipeTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeTableExists(int id)
        {
          return _context.recipesTable.Any(e => e.Id == id);
        }
    }
}
