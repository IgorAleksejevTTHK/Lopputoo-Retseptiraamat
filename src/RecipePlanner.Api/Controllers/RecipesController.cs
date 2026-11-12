using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RecipePlanner.Api.Dtos;
using RecipePlanner.Api.Services;

namespace RecipePlanner.Api.Controllers;

[ApiController]
[Route("api/recipes")]
[Produces("application/json")]
public class RecipesController(IRecipeService service) : ControllerBase
{
    // Until authentication is added every new recipe belongs to the seeded demo user.
    private const int DemoUserId = 2;

    /// <summary>Search, filter, sort and page recipes.</summary>
    /// <remarks>Example: <c>GET /api/recipes?search=soup&amp;ingredient=potato&amp;sortBy=prepTime&amp;page=1&amp;pageSize=10</c></remarks>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<RecipeListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<RecipeListItemDto>>> GetAll([FromQuery] RecipeQueryDto query, CancellationToken ct) =>
        Ok(await service.GetRecipesAsync(query, ct));

    /// <summary>Get one recipe with ingredients and steps.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RecipeDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDetailsDto>> GetById(int id, CancellationToken ct)
    {
        var recipe = await service.GetRecipeAsync(id, ct);
        return recipe is null ? NotFound() : Ok(recipe);
    }

    /// <summary>Create a recipe.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(RecipeDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RecipeDetailsDto>> Create(RecipeCreateUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var created = await service.CreateAsync(dto, DemoUserId, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Replace a recipe.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RecipeDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RecipeDetailsDto>> Update(int id, RecipeCreateUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var updated = await service.UpdateAsync(id, dto, ct);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Delete a recipe.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct) =>
        await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
