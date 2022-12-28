CREATE TABLE [dbo].[Recipe]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [RecipeName] NVARCHAR(50) NOT NULL, 
    [RecipeType] NVARCHAR(50) NOT NULL, 
    [Method] NVARCHAR(MAX) NOT NULL, 
    [PortionSize] INT NULL, 
    [CookingTime] INT NOT NULL, 
    [TotalCalories] INT NULL, 
    [Protein] INT NULL, 
    [Carbohydrates] INT NULL, 
    [Fats] INT NULL, 
    [ImageUrl] NVARCHAR(50) NULL, 
    [IngredientId] INT NULL

)
