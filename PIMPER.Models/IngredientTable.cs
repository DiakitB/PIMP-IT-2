using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace PIMPER.Models
{
	public class IngredientTable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [ValidateNever]
        public int UnitTableId { get; set; }
        [ValidateNever]
        public virtual UnitTable UnitTable { get; set; }
        [Required]
        public int QuantitiesTableId { get; set; }
        [ValidateNever]
        public virtual QuantitiesTable QuantitiesTable { get; set; }
        [ValidateNever]
        public virtual ICollection<RecipeIngredientTable> RecipeIngredientTable { get; set; }
    }
}
