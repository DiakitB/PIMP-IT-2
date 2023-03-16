using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PIMPER.DataP;
using PIMPER.Models;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList;

namespace PIMPER.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var recipes = from s in _context.recipesTable select s;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			//IEnumerable<RecipeTable> recipeTables = _context.recipesTable;
			if (!String.IsNullOrEmpty(searchString))
			{
				recipes = recipes.Where(s => s.Name.Contains(searchString));
			}
            switch (sortOrder)
            {
                case "name_desc":
                    recipes = recipes.OrderByDescending(s => s.Name);
                    break;
                default:
                    recipes = recipes.OrderBy(s => s.Name);
                    break;

			}
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(recipes.ToPagedList(pageNumber, pageSize));
		}
        public IActionResult Details(int recipeId)
        {
            UserBookMark cartObj = new()
            {
                Count = 1,
                RecipeTableId = recipeId,
                RecipeTable = _context.recipesTable
                .Include(u => u.RecipeIngredientTable)
                .ThenInclude(u => u.IngredientTable.UnitTable).Include(u => u.RecipeIngredientTable).ThenInclude(u => u.IngredientTable.QuantitiesTable)
                .AsNoTracking().FirstOrDefault(u => u.Id == recipeId)
            };
            return View(cartObj);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public IActionResult Details(UserBookMark userBookMark)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            userBookMark.ApplicationUserId = claim.Value;

            _context.userBookMarks.Add(userBookMark);
            _context.SaveChanges();
            return RedirectToAction("Index");
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