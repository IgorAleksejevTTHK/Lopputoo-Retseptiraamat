using System.ComponentModel.DataAnnotations;
using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Models;
using RecipePlanner.Api.Repositories;

namespace RecipePlanner.Api.Services;

public interface IRecipeService
{
    Task<PagedResult<RecipeListItemDto>> GetRecipesAsync(RecipeQueryDto query, CancellationToken ct = default);
    Task<RecipeDetailsDto?> GetRecipeAsync(int id, CancellationToken ct = default);
    Task<RecipeDetailsDto> CreateAsync(RecipeCreateUpdateDto dto, int ownerId, CancellationToken ct = default);
    Task<RecipeDetailsDto?> UpdateAsync(int id, RecipeCreateUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}

public class RecipeService(
    IRecipeRepository recipes,
    IIngredientRepository ingredients,
    ICategoryRepository categories) : IRecipeService
{
    public async Task<PagedResult<RecipeListItemDto>> GetRecipesAsync(RecipeQueryDto query, CancellationToken ct = default)
    {
        var (items, total) = await recipes.GetPagedAsync(query, ct);
        var dtos = items.Select(r => new RecipeListItemDto(
            r.Id, r.Title, r.Description, r.Servings, r.PrepTimeMinutes,
            r.Category?.Name ?? "", r.Owner?.UserName ?? "", r.Ingredients.Count)).ToList();
        return new PagedResult<RecipeListItemDto>(dtos, query.Page, query.PageSize, total);
    }

    public async Task<RecipeDetailsDto?> GetRecipeAsync(int id, CancellationToken ct = default)
    {
        var recipe = await recipes.GetByIdAsync(id, ct);
        return recipe is null ? null : ToDetails(recipe);
    }

    public async Task<RecipeDetailsDto> CreateAsync(RecipeCreateUpdateDto dto, int ownerId, CancellationToken ct = default)
    {
        await EnsureCategoryExists(dto.CategoryId, ct);

        var recipe = new Recipe { OwnerId = ownerId, CreatedAt = DateTime.UtcNow };
        await Apply(recipe, dto, ct);
        await recipes.AddAsync(recipe, ct);
        await recipes.SaveChangesAsync(ct);

        return (await GetRecipeAsync(recipe.Id, ct))!;
    }

    public async Task<RecipeDetailsDto?> UpdateAsync(int id, RecipeCreateUpdateDto dto, CancellationToken ct = default)
    {
        var recipe = await recipes.GetByIdAsync(id, ct);
        if (recipe is null) return null;

        await EnsureCategoryExists(dto.CategoryId, ct);

        recipe.Ingredients.Clear();
        recipe.Steps.Clear();
        await Apply(recipe, dto, ct);
        await recipes.SaveChangesAsync(ct);

        return (await GetRecipeAsync(id, ct))!;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var recipe = await recipes.GetByIdAsync(id, ct);
        if (recipe is null) return false;
        recipes.Remove(recipe);
        await recipes.SaveChangesAsync(ct);
        return true;
    }

    private async Task EnsureCategoryExists(int categoryId, CancellationToken ct)
    {
        if (!await categories.ExistsAsync(categoryId, ct))
            throw new ValidationException($"Category {categoryId} does not exist.");
    }

    private async Task Apply(Recipe recipe, RecipeCreateUpdateDto dto, CancellationToken ct)
    {
        var duplicate = dto.Ingredients
            .GroupBy(i => i.Name.Trim(), StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault(g => g.Count() > 1);
        if (duplicate is not null)
            throw new ValidationException($"Ingredient '{duplicate.Key}' is listed more than once.");

        recipe.Title = dto.Title.Trim();
        recipe.Description = dto.Description?.Trim();
        recipe.Servings = dto.Servings;
        recipe.PrepTimeMinutes = dto.PrepTimeMinutes;
        recipe.CategoryId = dto.CategoryId;

        foreach (var input in dto.Ingredients)
        {
            // Reuse an existing dictionary ingredient or create a new one.
            var ingredient = await ingredients.FindByNameAsync(input.Name, ct)
                             ?? new Ingredient { Name = input.Name.Trim(), DefaultUnit = input.Unit.Trim() };
            recipe.Ingredients.Add(new RecipeIngredient
            {
                Ingredient = ingredient,
                Quantity = input.Quantity,
                Unit = input.Unit.Trim()
            });
        }

        var step = 1;
        foreach (var s in dto.Steps)
            recipe.Steps.Add(new RecipeStep { StepNumber = step++, Instruction = s.Instruction.Trim() });
    }

    private static RecipeDetailsDto ToDetails(Recipe r) => new(
        r.Id, r.Title, r.Description, r.Servings, r.PrepTimeMinutes,
        r.CategoryId, r.Category?.Name ?? "", r.OwnerId, r.Owner?.UserName ?? "", r.CreatedAt,
        r.Ingredients.Select(i => new RecipeIngredientDto(i.IngredientId, i.Ingredient?.Name ?? "", i.Quantity, i.Unit)).ToList(),
        r.Steps.OrderBy(s => s.StepNumber).Select(s => new RecipeStepDto(s.StepNumber, s.Instruction)).ToList());
}
