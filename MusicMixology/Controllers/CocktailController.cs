using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.Services;

namespace MusicMixology.ApiControllers
{
    [Route("api/cocktails")]
    [ApiController]
    public class CocktailController : ControllerBase
    {
        private readonly ICocktailService _service;

        public CocktailController(ICocktailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailDTO>>> GetCocktails()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailDTO>> GetCocktail(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CocktailDTO>> PostCocktail(CocktailDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCocktail), new { id = result.CocktailID }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCocktail(int id, CocktailDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
