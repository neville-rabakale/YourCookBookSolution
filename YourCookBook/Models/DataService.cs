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
            return await context.Recipes
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
        }
        public async Task AddIngredientsAsync(DisplayIngredientModel model, DisplayRecipeModel recipeModel)
        {
            await context.Ingredients.AddAsync(new Ingredient
            {
                IngredientName = model.IngredientName,
                Amount = model.Amount,
                RecipeId = recipeModel.Id
            });

            await context.SaveChangesAsync();
        }
        public async Task AddRecipeAsync(DisplayRecipeModel recipeModel)
        {

//            UploadImageAsync(recipeModel);
            var imagepath = @"images\recipes\" + recipeModel.ImageUrl.Name;

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

        }




    }
}
