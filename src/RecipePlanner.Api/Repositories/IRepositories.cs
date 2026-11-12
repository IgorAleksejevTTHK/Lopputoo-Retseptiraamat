using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Models;

namespace RecipePlanner.Api.Repositories;

public interface IRecipeRepository
{
    Task<(IReadOnlyList<Recipe> Items, int Total)> GetPagedAsync(RecipeQueryDto query, CancellationToken ct = default);
    Task<Recipe?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Recipe recipe, CancellationToken ct = default);
    void Remove(Recipe recipe);
    Task SaveChangesAsync(CancellationToken ct = default);
}

public interface IIngredientRepository
{
    Task<IReadOnlyList<Ingredient>> GetAllAsync(string? search, CancellationToken ct = default);
    Task<Ingredient?> FindByNameAsync(string name, CancellationToken ct = default);
}

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
}
