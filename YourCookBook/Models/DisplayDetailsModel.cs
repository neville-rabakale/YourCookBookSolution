using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DisplayDetailsModel
    {

       [Display(Name = "Recipe Name")]
        public string RecipeName { get; set; } = null!;
        public string RecipeType { get; set; }

        [Display(Name = "Method")]
        public string Method { get; set; } = null!;
        public int? PortionSize { get; set; }

        [Required(ErrorMessage = "Please enter cooking time in minutes")]
        public int CookingTime { get; set; }

        public int? TotalCalories { get; set; }

        public int? Protein { get; set; }

        public int? Carbohydrates { get; set; }

        public int? Fats { get; set; }

        public string? ImageUrl { get; set; } = null;

        public virtual ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
    }
}
