using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DisplayRecipeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the recipe")]
        [Display(Name = "Recipe Name")]
        public string RecipeName { get; set; } = null!;
        public int RecipeTypeValue { get; set; } 

        [Required(ErrorMessage = "Please enter the method of how to cook the recipe")]
        [Display(Name = "Method")]
        public string Method { get; set; } = null!;

        public int? PortionSize { get; set; }

        [Required(ErrorMessage ="Please enter cooking time in minutes")]
        public int CookingTime { get; set; }

        public int? TotalCalories { get; set; }

        public int? Protein { get; set; }

        public int? Carbohydrates { get; set; }

        public int? Fats { get; set; }

        public IBrowserFile? ImageUrl { get; set; } = null;

        public virtual ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
    }
}
