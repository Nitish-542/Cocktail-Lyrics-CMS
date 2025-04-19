using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.Services;

namespace MusicMixology.ApiControllers
{
    // API controller for managing cocktail-related operations
    [Route("api/cocktails")]
    [ApiController]
    public class CocktailController : ControllerBase
    {
        private readonly ICocktailService _service;

        // Constructor that injects the cocktail service dependency
        public CocktailController(ICocktailService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all cocktails.
        /// </summary>
        /// <returns>List of CocktailDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailDTO>>> GetCocktails()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific cocktail by its ID.
        /// </summary>
        /// <param name="id">The ID of the cocktail.</param>
        /// <returns>CocktailDTO if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailDTO>> GetCocktail(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new cocktail.
        /// </summary>
        /// <param name="dto">CocktailDTO containing cocktail details.</param>
        /// <returns>Newly created CocktailDTO with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<CocktailDTO>> PostCocktail(CocktailDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCocktail), new { id = result.CocktailID }, result);
        }

        /// <summary>
        /// Updates an existing cocktail by ID.
        /// </summary>
        /// <param name="id">ID of the cocktail to update.</param>
        /// <param name="dto">Updated CocktailDTO object.</param>
        /// <returns>NoContent if update is successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCocktail(int id, CocktailDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a cocktail by ID.
        /// </summary>
        /// <param name="id">ID of the cocktail to delete.</param>
        /// <returns>NoContent if deletion is successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
