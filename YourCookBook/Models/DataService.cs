using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourCookBook.Models.Entities;

namespace YourCookBook.Models
{
    public class DataService
    {
        private readonly CookBookContext context;
        private readonly IWebHostEnvironment environment;

        public DataService(CookBookContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        //public async Task UploadImageAsync(DisplayRecipeModel model)
        //{
        //    // string fileName = Guid.NewGuid().ToString(); //to get a new unique file name
        //    var filePath = Path.Combine(environment.WebRootPath, @"images\recipes", model.ImageUrl.FileName);

        //    using var fileStream = new FileStream(filePath, FileMode.Create);
        //    await model.ImageUrl.CopyToAsync(fileStream);

        //}


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

        //public Recipe GetRecipe(int id)
        //{
            
        //}
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
