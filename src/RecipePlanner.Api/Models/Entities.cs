namespace RecipePlanner.Api.Models;

/// <summary>Application user. Owns recipes and meal plans.</summary>
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}

public static class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";
    public const string Service = "Service";
}

/// <summary>Recipe category, e.g. soup, dessert.</summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}

/// <summary>Ingredient dictionary entry (shared between recipes).</summary>
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DefaultUnit { get; set; } = "g";

    public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}

public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    /// <summary>Number of servings the ingredient quantities are written for.</summary>
    public int Servings { get; set; } = 4;
    public int PrepTimeMinutes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int OwnerId { get; set; }
    public User? Owner { get; set; }

    public ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
    public ICollection<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
}

/// <summary>Join table: ingredient with quantity inside a recipe.</summary>
public class RecipeIngredient
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = "g";
}

public class RecipeStep
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
    public int StepNumber { get; set; }
    public string Instruction { get; set; } = string.Empty;
}
