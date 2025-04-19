using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    // API controller for managing cocktail-song pairings.
    [Route("api/pairings")]
    [ApiController]
    public class CocktailSongPairingController : ControllerBase
    {
        private readonly ICocktailSongPairingService _service;

        // Constructor with dependency injection for cocktail-song pairing service.
        public CocktailSongPairingController(ICocktailSongPairingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all cocktail-song pairings.
        /// </summary>
        /// <returns>List of CocktailSongPairingDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailSongPairingDTO>>> GetPairings()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a single cocktail-song pairing by its ID.
        /// </summary>
        /// <param name="id">Pairing ID</param>
        /// <returns>CocktailSongPairingDTO object if found; otherwise, NotFound.</returns>
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
        /// <param name="dto">CocktailSongPairingDTO object with pairing details.</param>
        /// <returns>Created CocktailSongPairingDTO object with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<CocktailSongPairingDTO>> PostPairing(CocktailSongPairingDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPairing), new { id = created.PairingId }, created);
        }

        /// <summary>
        /// Updates an existing cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">Pairing ID</param>
        /// <param name="dto">Updated CocktailSongPairingDTO object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
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
        /// <param name="id">Pairing ID</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePairing(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
