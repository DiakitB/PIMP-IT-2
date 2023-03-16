using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PIMPER.Models
{
    public class RecipeIngredientTable
    {
        [Key]

        public int Id { get; set; }

        public int IngredientTableId { get; set; }
        [ValidateNever]
        public IngredientTable IngredientTable { get; set; }

        public int RecipeTableId { get; set; }
        [ValidateNever]
        public virtual RecipeTable RecipeTable { get; set; }
    }
}
