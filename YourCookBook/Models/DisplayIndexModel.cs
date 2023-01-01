using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DisplayIndexModel
    {
        public int Id { get; set; }

        public string RecipeName { get; set; } = null!;

        public int CookingTime { get; set; }
        public int? TotalCalories { get; set; }

        public string RecipeType { get; set; }

        public string? ImageUrl { get; set; }

    }
}
