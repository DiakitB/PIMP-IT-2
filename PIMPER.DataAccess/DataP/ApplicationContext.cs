using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PIMPER.Models;

namespace PIMPER.DataP
{
	public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<RecipeTable> recipesTable { get; set; }
        public DbSet<RecipeIngredientTable> recipeIngredientTables { get; set; }
        public DbSet<IngredientTable> ingredientTables { get; set; }
        public DbSet<UnitTable> unitTables { get; set; }    
        public DbSet<QuantitiesTable> quantitiesTables { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserBookMark> userBookMarks { get; set; }
    }
}
