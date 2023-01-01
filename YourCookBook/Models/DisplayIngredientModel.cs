using System.ComponentModel.DataAnnotations;
using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DisplayIngredientModel
    {
        [Required(ErrorMessage ="Please enter the name of the ingredient")]
        [Display(Name ="Ingredient")]
        public string IngredientName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter the set amount of the ingredient and a unit, e.g. 10g")]
        [Display(Name = "Quantity")]
        public string Amount { get; set; } = null!;
        public int? RecipeId { get; set; }
        public virtual Recipe? Recipe { get; set; }

    }
}
