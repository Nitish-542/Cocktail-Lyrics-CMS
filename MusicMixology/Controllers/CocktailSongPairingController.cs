using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    /// <summary>
    /// API controller for managing cocktail and song pairings.
    /// Provides endpoints to perform CRUD operations on pairings.
    /// </summary>
    [Route("api/pairings")]
    [ApiController]
    public class CocktailSongPairingController : ControllerBase
    {
        private readonly ICocktailSongPairingService _service;

        /// <summary>
        /// Constructor for injecting the pairing service dependency.
        /// </summary>
        /// <param name="service">Service to handle cocktail-song pairing logic.</param>
        public CocktailSongPairingController(ICocktailSongPairingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all cocktail-song pairings.
        /// </summary>
        /// <returns>A list of all pairings.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailSongPairingDTO>>> GetPairings()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets a specific cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing.</param>
        /// <returns>The matching pairing, or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailSongPairingDTO>> GetPairing(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Creates a new cocktail-song pairing.
        /// </summary>
        /// <param name="dto">The pairing data transfer object.</param>
        /// <returns>The created pairing with its new ID.</returns>
        [HttpPost]
        public async Task<ActionResult<CocktailSongPairingDTO>> PostPairing(CocktailSongPairingDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPairing), new { id = created.PairingId }, created);
        }

        /// <summary>
        /// Updates an existing cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to update.</param>
        /// <param name="dto">The updated pairing data.</param>
        /// <returns>No content if successful, or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPairing(int id, CocktailSongPairingDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to delete.</param>
        /// <returns>No content if deleted, or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePairing(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
