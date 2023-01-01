using System;
using System.Collections.Generic;

namespace YourCookBook.Models.Entities;

public partial class Recipe
{
    public int Id { get; set; }

    public string RecipeName { get; set; } = null!;

    public int RecipeType { get; set; }

    public string Method { get; set; } = null!;

    public int? PortionSize { get; set; }

    public int CookingTime { get; set; }

    public int? TotalCalories { get; set; }

    public int? Protein { get; set; }

    public int? Carbohydrates { get; set; }

    public int? Fats { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Ingredient> Ingredients { get; } = new List<Ingredient>();
}
