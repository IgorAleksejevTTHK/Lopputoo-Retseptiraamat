using System.ComponentModel.DataAnnotations;

namespace RecipePlanner.Api.Dtos;

/// <summary>Short recipe info for lists.</summary>
public record RecipeListItemDto(
    int Id, string Title, string? Description, int Servings, int PrepTimeMinutes,
    string Category, string Owner, int IngredientCount);

public record RecipeIngredientDto(int IngredientId, string Name, decimal Quantity, string Unit);

public record RecipeStepDto(int StepNumber, string Instruction);

/// <summary>Full recipe with ingredients and steps.</summary>
public record RecipeDetailsDto(
    int Id, string Title, string? Description, int Servings, int PrepTimeMinutes,
    int CategoryId, string Category, int OwnerId, string Owner, DateTime CreatedAt,
    IReadOnlyList<RecipeIngredientDto> Ingredients, IReadOnlyList<RecipeStepDto> Steps);

public class RecipeIngredientInputDto
{
    /// <example>Potato</example>
    [Required, StringLength(80, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    /// <example>500</example>
    [Range(0.01, 100000)]
    public decimal Quantity { get; set; }

    /// <example>g</example>
    [Required, StringLength(20, MinimumLength = 1)]
    public string Unit { get; set; } = string.Empty;
}

public class RecipeStepInputDto
{
    /// <example>Peel and dice the potatoes.</example>
    [Required, StringLength(1000, MinimumLength = 3)]
    public string Instruction { get; set; } = string.Empty;
}

/// <summary>Body for creating or updating a recipe.</summary>
public class RecipeCreateUpdateDto
{
    /// <example>Potato soup</example>
    [Required, StringLength(120, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    /// <example>Simple and warm soup.</example>
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <example>4</example>
    [Range(1, 50)]
    public int Servings { get; set; } = 4;

    /// <example>40</example>
    [Range(1, 1440)]
    public int PrepTimeMinutes { get; set; }

    /// <example>2</example>
    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [Required, MinLength(1)]
    public List<RecipeIngredientInputDto> Ingredients { get; set; } = new();

    [Required, MinLength(1)]
    public List<RecipeStepInputDto> Steps { get; set; } = new();
}

/// <summary>Query string for recipe list: search, filter, sort, pagination.</summary>
public class RecipeQueryDto
{
    /// <summary>Text search in title and description.</summary>
    public string? Search { get; set; }
    /// <summary>Only recipes containing this ingredient (name, partial match).</summary>
    public string? Ingredient { get; set; }
    public int? CategoryId { get; set; }
    /// <summary>Max preparation time in minutes.</summary>
    public int? MaxPrepTime { get; set; }
    /// <summary>title | prepTime | createdAt | servings</summary>
    public string SortBy { get; set; } = "title";
    /// <summary>asc | desc</summary>
    public string SortDir { get; set; } = "asc";
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;
    [Range(1, 100)]
    public int PageSize { get; set; } = 10;
}

public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
}

public record CategoryDto(int Id, string Name);

public record IngredientDto(int Id, string Name, string DefaultUnit);
