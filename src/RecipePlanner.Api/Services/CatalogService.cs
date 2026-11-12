using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Repositories;

namespace RecipePlanner.Api.Services;

/// <summary>Read-only dictionaries: categories and ingredients.</summary>
public interface ICatalogService
{
    Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<IngredientDto>> GetIngredientsAsync(string? search, CancellationToken ct = default);
}

public class CatalogService(ICategoryRepository categories, IIngredientRepository ingredients) : ICatalogService
{
    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync(CancellationToken ct = default) =>
        (await categories.GetAllAsync(ct)).Select(c => new CategoryDto(c.Id, c.Name)).ToList();

    public async Task<IReadOnlyList<IngredientDto>> GetIngredientsAsync(string? search, CancellationToken ct = default) =>
        (await ingredients.GetAllAsync(search, ct)).Select(i => new IngredientDto(i.Id, i.Name, i.DefaultUnit)).ToList();
}
