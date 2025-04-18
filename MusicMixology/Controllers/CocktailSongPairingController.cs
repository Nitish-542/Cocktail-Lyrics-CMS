using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    [Route("api/pairings")]
    [ApiController]
    public class CocktailSongPairingController : ControllerBase
    {
        private readonly ICocktailSongPairingService _service;

        public CocktailSongPairingController(ICocktailSongPairingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailSongPairingDTO>>> GetPairings()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailSongPairingDTO>> GetPairing(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CocktailSongPairingDTO>> PostPairing(CocktailSongPairingDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPairing), new { id = created.PairingId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPairing(int id, CocktailSongPairingDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePairing(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
