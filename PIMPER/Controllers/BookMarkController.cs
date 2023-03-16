using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PIMPER.DataP;
using PIMPER.Models;
using System.Security.Claims;

namespace PIMPER.Controllers
{
	[Authorize]
	public class BookMarkController : Controller
	{
		private readonly ApplicationContext _context;
		public BookMarkVM BookMarkVM { get; set; }
        public BookMarkController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			IEnumerable<UserBookMark> bookmarks = _context.userBookMarks.Where(u => u.ApplicationUserId == claim.Value).Include(u => u.RecipeTable);

			return View(bookmarks);
		}
		public IActionResult Details2(int? id)
		{

			var  fovoriteListe = _context.userBookMarks.Include(u => u.RecipeTable.RecipeIngredientTable)
				.ThenInclude(x=>x.IngredientTable.QuantitiesTable)
				.Include(v=>v.RecipeTable.RecipeIngredientTable)
				.ThenInclude(y=>y.IngredientTable.UnitTable).AsNoTracking().FirstOrDefault(t=>t.Id == id);
			if(fovoriteListe == null)
			{
				return NotFound();
			}

			return View(fovoriteListe);
		}

        public IActionResult Delete(int? id)
        {
            if (id == null || _context.userBookMarks == null)
            {
                return NotFound();
            }
            var deletitem = _context.userBookMarks.FirstOrDefault(u=> u.Id == id);
            if (deletitem == null)
            {
                return BadRequest();
            }


            return View(deletitem);
        }

        // POST: IngredientTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var deleConfirm = _context.userBookMarks.FirstOrDefault(u=>u.Id == id);
            if(deleConfirm != null)
            {
                _context.userBookMarks.Remove(deleConfirm);
                _context.SaveChanges();
            }
           return RedirectToAction("Index");
        }
    }
}
//_context.recipesTable
//				.Include(u => u.RecipeIngredientTable)
//				.ThenInclude(u => u.IngredientTable.UnitTable).Include(u => u.RecipeIngredientTable).ThenInclude(u => u.IngredientTable.QuantitiesTable)
//				.AsNoTracking().FirstOrDefault(u => u.Id == recipeId)
//            };