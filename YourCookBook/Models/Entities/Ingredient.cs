using System;
using System.Collections.Generic;

namespace YourCookBook.Models.Entities;

public partial class Ingredient
{
    public int Id { get; set; }

    public string IngredientName { get; set; } = null!;

    public string Amount { get; set; } = null!;

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
