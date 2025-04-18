using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    [Route("api/bartenders")]
    [ApiController]
    public class BartenderController : ControllerBase
    {
        private readonly IBartenderService _service;

        public BartenderController(IBartenderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BartenderDto>>> GetBartenders()
        {
            var bartenders = await _service.GetAllAsync();
            return Ok(bartenders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BartenderDto>> GetBartender(int id)
        {
            var bartender = await _service.GetByIdAsync(id);
            if (bartender == null) return NotFound();
            return Ok(bartender);
        }

        [HttpPost]
        public async Task<ActionResult<BartenderDto>> PostBartender(BartenderDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetBartender), new { id = result.BartenderId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBartender(int id, BartenderDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBartender(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
