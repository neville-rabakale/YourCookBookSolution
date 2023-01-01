CREATE TABLE [dbo].[Ingredients]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [IngredientName] NVARCHAR(50) NOT NULL, 
    [Amount] NVARCHAR(50) NOT NULL, 
    [RecipeId] INT NULL, 
    CONSTRAINT [FK_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe]([Id])
)
