using Microsoft.AspNetCore.Mvc;
using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Services;

namespace RecipePlanner.Api.Controllers;

[ApiController]
[Route("api")]
[Produces("application/json")]
public class CatalogController(ICatalogService service) : ControllerBase
{
    /// <summary>All recipe categories.</summary>
    [HttpGet("categories")]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories(CancellationToken ct) =>
        Ok(await service.GetCategoriesAsync(ct));

    /// <summary>Ingredient dictionary, optional name search.</summary>
    [HttpGet("ingredients")]
    [ProducesResponseType(typeof(IReadOnlyList<IngredientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<IngredientDto>>> GetIngredients([FromQuery] string? search, CancellationToken ct) =>
        Ok(await service.GetIngredientsAsync(search, ct));
}
