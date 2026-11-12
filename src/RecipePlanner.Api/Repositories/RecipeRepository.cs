using Microsoft.EntityFrameworkCore;
using RecipePlanner.Api.Data;
using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Models;

namespace RecipePlanner.Api.Repositories;

public class RecipeRepository(AppDbContext db) : IRecipeRepository
{
    public async Task<(IReadOnlyList<Recipe> Items, int Total)> GetPagedAsync(RecipeQueryDto q, CancellationToken ct = default)
    {
        IQueryable<Recipe> query = db.Recipes.AsNoTracking()
            .Include(r => r.Category)
            .Include(r => r.Owner)
            .Include(r => r.Ingredients);

        if (!string.IsNullOrWhiteSpace(q.Search))
        {
            var s = q.Search.Trim().ToLower();
            query = query.Where(r => r.Title.ToLower().Contains(s) ||
                                     (r.Description != null && r.Description.ToLower().Contains(s)));
        }

        if (!string.IsNullOrWhiteSpace(q.Ingredient))
        {
            var ing = q.Ingredient.Trim().ToLower();
            query = query.Where(r => r.Ingredients.Any(i => i.Ingredient!.Name.ToLower().Contains(ing)));
        }

        if (q.CategoryId is not null)
            query = query.Where(r => r.CategoryId == q.CategoryId);

        if (q.MaxPrepTime is not null)
            query = query.Where(r => r.PrepTimeMinutes <= q.MaxPrepTime);

        var desc = string.Equals(q.SortDir, "desc", StringComparison.OrdinalIgnoreCase);
        query = q.SortBy.ToLowerInvariant() switch
        {
            "preptime" => desc ? query.OrderByDescending(r => r.PrepTimeMinutes) : query.OrderBy(r => r.PrepTimeMinutes),
            "createdat" => desc ? query.OrderByDescending(r => r.CreatedAt) : query.OrderBy(r => r.CreatedAt),
            "servings" => desc ? query.OrderByDescending(r => r.Servings) : query.OrderBy(r => r.Servings),
            _ => desc ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title)
        };

        var total = await query.CountAsync(ct);
        var items = await query.Skip((q.Page - 1) * q.PageSize).Take(q.PageSize).ToListAsync(ct);
        return (items, total);
    }

    public Task<Recipe?> GetByIdAsync(int id, CancellationToken ct = default) =>
        db.Recipes
            .Include(r => r.Category)
            .Include(r => r.Owner)
            .Include(r => r.Ingredients).ThenInclude(i => i.Ingredient)
            .Include(r => r.Steps)
            .FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task AddAsync(Recipe recipe, CancellationToken ct = default) =>
        await db.Recipes.AddAsync(recipe, ct);

    public void Remove(Recipe recipe) => db.Recipes.Remove(recipe);

    public Task SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct);
}

public class IngredientRepository(AppDbContext db) : IIngredientRepository
{
    public async Task<IReadOnlyList<Ingredient>> GetAllAsync(string? search, CancellationToken ct = default)
    {
        var q = db.Ingredients.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim().ToLower();
            q = q.Where(i => i.Name.ToLower().Contains(s));
        }
        return await q.OrderBy(i => i.Name).ToListAsync(ct);
    }

    public Task<Ingredient?> FindByNameAsync(string name, CancellationToken ct = default)
    {
        var n = name.Trim().ToLower();
        return db.Ingredients.FirstOrDefaultAsync(i => i.Name.ToLower() == n, ct);
    }
}

public class CategoryRepository(AppDbContext db) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default) =>
        await db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync(ct);

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
        db.Categories.AnyAsync(c => c.Id == id, ct);
}
