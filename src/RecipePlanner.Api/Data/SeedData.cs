using Microsoft.EntityFrameworkCore;
using RecipePlanner.Api.Models;

namespace RecipePlanner.Api.Data;

/// <summary>Initial data inserted by migrations (HasData).</summary>
public static class SeedData
{
    private static readonly DateTime Seeded = new(2026, 11, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void Apply(ModelBuilder b)
    {
        b.Entity<User>().HasData(
            new User { Id = 1, UserName = "admin", Email = "admin@recipes.local", Role = Roles.Admin, CreatedAt = Seeded },
            new User { Id = 2, UserName = "mari", Email = "mari@recipes.local", Role = Roles.User, CreatedAt = Seeded });

        b.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Breakfast" },
            new Category { Id = 2, Name = "Soup" },
            new Category { Id = 3, Name = "Main course" },
            new Category { Id = 4, Name = "Dessert" });

        b.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Oat flakes", DefaultUnit = "g" },
            new Ingredient { Id = 2, Name = "Milk", DefaultUnit = "ml" },
            new Ingredient { Id = 3, Name = "Potato", DefaultUnit = "g" },
            new Ingredient { Id = 4, Name = "Onion", DefaultUnit = "pcs" },
            new Ingredient { Id = 5, Name = "Carrot", DefaultUnit = "pcs" },
            new Ingredient { Id = 6, Name = "Chicken fillet", DefaultUnit = "g" },
            new Ingredient { Id = 7, Name = "Salt", DefaultUnit = "tsp" },
            new Ingredient { Id = 8, Name = "Egg", DefaultUnit = "pcs" },
            new Ingredient { Id = 9, Name = "Wheat flour", DefaultUnit = "g" },
            new Ingredient { Id = 10, Name = "Sugar", DefaultUnit = "g" },
            new Ingredient { Id = 11, Name = "Butter", DefaultUnit = "g" },
            new Ingredient { Id = 12, Name = "Pasta", DefaultUnit = "g" });

        b.Entity<Recipe>().HasData(
            new Recipe { Id = 1, Title = "Oatmeal porridge", Description = "Classic Estonian breakfast porridge.", Servings = 2, PrepTimeMinutes = 10, CategoryId = 1, OwnerId = 2, CreatedAt = Seeded },
            new Recipe { Id = 2, Title = "Chicken soup", Description = "Light soup with vegetables.", Servings = 4, PrepTimeMinutes = 50, CategoryId = 2, OwnerId = 2, CreatedAt = Seeded },
            new Recipe { Id = 3, Title = "Pancakes", Description = "Thin pancakes.", Servings = 4, PrepTimeMinutes = 30, CategoryId = 4, OwnerId = 1, CreatedAt = Seeded },
            new Recipe { Id = 4, Title = "Chicken pasta", Description = "Quick weekday dinner.", Servings = 3, PrepTimeMinutes = 25, CategoryId = 3, OwnerId = 1, CreatedAt = Seeded });

        b.Entity<RecipeIngredient>().HasData(
            new RecipeIngredient { Id = 1, RecipeId = 1, IngredientId = 1, Quantity = 100, Unit = "g" },
            new RecipeIngredient { Id = 2, RecipeId = 1, IngredientId = 2, Quantity = 400, Unit = "ml" },
            new RecipeIngredient { Id = 3, RecipeId = 1, IngredientId = 7, Quantity = 0.5m, Unit = "tsp" },
            new RecipeIngredient { Id = 4, RecipeId = 2, IngredientId = 6, Quantity = 400, Unit = "g" },
            new RecipeIngredient { Id = 5, RecipeId = 2, IngredientId = 3, Quantity = 500, Unit = "g" },
            new RecipeIngredient { Id = 6, RecipeId = 2, IngredientId = 4, Quantity = 1, Unit = "pcs" },
            new RecipeIngredient { Id = 7, RecipeId = 2, IngredientId = 5, Quantity = 2, Unit = "pcs" },
            new RecipeIngredient { Id = 8, RecipeId = 2, IngredientId = 7, Quantity = 1, Unit = "tsp" },
            new RecipeIngredient { Id = 9, RecipeId = 3, IngredientId = 9, Quantity = 200, Unit = "g" },
            new RecipeIngredient { Id = 10, RecipeId = 3, IngredientId = 2, Quantity = 500, Unit = "ml" },
            new RecipeIngredient { Id = 11, RecipeId = 3, IngredientId = 8, Quantity = 2, Unit = "pcs" },
            new RecipeIngredient { Id = 12, RecipeId = 3, IngredientId = 10, Quantity = 30, Unit = "g" },
            new RecipeIngredient { Id = 13, RecipeId = 3, IngredientId = 11, Quantity = 20, Unit = "g" },
            new RecipeIngredient { Id = 14, RecipeId = 4, IngredientId = 12, Quantity = 300, Unit = "g" },
            new RecipeIngredient { Id = 15, RecipeId = 4, IngredientId = 6, Quantity = 300, Unit = "g" },
            new RecipeIngredient { Id = 16, RecipeId = 4, IngredientId = 4, Quantity = 1, Unit = "pcs" });

        b.Entity<RecipeStep>().HasData(
            new RecipeStep { Id = 1, RecipeId = 1, StepNumber = 1, Instruction = "Bring the milk to a boil." },
            new RecipeStep { Id = 2, RecipeId = 1, StepNumber = 2, Instruction = "Add oat flakes and salt, cook for 5 minutes while stirring." },
            new RecipeStep { Id = 3, RecipeId = 2, StepNumber = 1, Instruction = "Boil the chicken in 2 litres of water for 20 minutes." },
            new RecipeStep { Id = 4, RecipeId = 2, StepNumber = 2, Instruction = "Add diced potatoes, carrots and onion." },
            new RecipeStep { Id = 5, RecipeId = 2, StepNumber = 3, Instruction = "Cook until vegetables are soft, season with salt." },
            new RecipeStep { Id = 6, RecipeId = 3, StepNumber = 1, Instruction = "Whisk eggs with sugar, add milk and flour." },
            new RecipeStep { Id = 7, RecipeId = 3, StepNumber = 2, Instruction = "Fry thin pancakes on buttered pan." },
            new RecipeStep { Id = 8, RecipeId = 4, StepNumber = 1, Instruction = "Cook pasta according to the package." },
            new RecipeStep { Id = 9, RecipeId = 4, StepNumber = 2, Instruction = "Fry chicken and onion, mix with pasta." });
    }
}
