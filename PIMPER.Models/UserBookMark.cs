using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMPER.Models
{
	public class UserBookMark
	{
		public int Id { get; set; }
		public int RecipeTableId { get; set; }
		[ForeignKey("RecipeTableId")]
		[ValidateNever]
		public RecipeTable RecipeTable { get; set; }
		public int Count { get; set; }
		public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
		[ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
	}
}
