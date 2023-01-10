using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DataService
    {
        private readonly CookBookContext context;

        public DataService(CookBookContext context)
        {
            this.context = context;
        }

        public static string GetRecipeText(int value)
        {
            if (value == 1)
                return "Breakfast";
            if (value == 2)
                return "Lunch";
            if (value == 3)
                return "Dinner";           
            if (value == 4)
                return "Drinks";           
            else 
                return "Dessert";
        }

        public async Task<DisplayIndexModel[]> GetAllRecipesAsync()
        {
            return await context.Recipes
                .Select(o => new DisplayIndexModel
                {
                    Id = o.Id,
                    RecipeName = o.RecipeName,
                    CookingTime = o.CookingTime,
                    RecipeType = GetRecipeText(o.RecipeType),
                    TotalCalories = o.TotalCalories,
                    ImageUrl = o.ImageUrl
                }).ToArrayAsync();
        }

        public async Task<DisplayDetailsModel> GetRecipeDetailsAsync(int id)
        {

            var newDetail = await context.Recipes.Where(o => o.Id == id)
                .Select(o => new DisplayDetailsModel
                {
                    RecipeName = o.RecipeName,
                    RecipeType = GetRecipeText(o.RecipeType),
                    Method = o.Method,
                    PortionSize = o.PortionSize,
                    CookingTime = o.CookingTime,
                    TotalCalories = o.TotalCalories,
                    Protein = o.Protein,
                    Carbohydrates = o.Carbohydrates,
                    Fats = o.Fats,
                    ImageUrl = o.ImageUrl
                }).SingleAsync();

            return newDetail;
        }

        public async Task AddIngredientsAsync(List<DisplayIngredientModel> model, int recipeId)
        {

            foreach (var item in model)
            {
                await context.Ingredients.AddAsync(new Ingredient
                {
                    IngredientName = item.IngredientName,
                    Amount = item.Amount,
                    RecipeId = recipeId
                });

            }
            await context.SaveChangesAsync();


        }

        public async Task<Ingredient[]> GetIngredientsAsync(int recipeId)
        {
            return await context.Ingredients.
                Where(o => o.RecipeId == recipeId)
                .ToArrayAsync();
        }
        public async Task AddRecipeAsync(DisplayRecipeModel recipeModel, List<DisplayIngredientModel> model)
        {

            var imagepath = @"images/recipes/" + recipeModel.ImageUrl.Name;

            await context.Recipes.AddAsync(new Recipe
            {
                ImageUrl = imagepath,
                RecipeName = recipeModel.RecipeName,
                RecipeType = recipeModel.RecipeTypeValue,
                Method = recipeModel.Method,
                PortionSize = recipeModel.PortionSize,
                CookingTime = recipeModel.CookingTime,
                TotalCalories = recipeModel.TotalCalories,
                Protein = recipeModel.Protein,
                Carbohydrates = recipeModel.Carbohydrates,
                Fats = recipeModel.Fats,
            });

            await context.SaveChangesAsync();

            var latestId = await context.Recipes.Where(o => o.Id != null)
                .OrderByDescending(o => o.Id)
                .Select(o => o.Id)
                .FirstAsync();

            foreach (var item in model)
            {
                await context.Ingredients.AddAsync(new Ingredient
                {
                    IngredientName = item.IngredientName,
                    Amount = item.Amount,
                    RecipeId= latestId,
                    
                });

            }

            await context.SaveChangesAsync();

        }

    }
}
