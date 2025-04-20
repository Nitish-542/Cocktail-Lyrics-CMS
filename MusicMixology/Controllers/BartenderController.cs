using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    /// <summary>
    /// API controller for managing bartender-related operations.
    /// </summary>
    [Route("api/bartenders")]
    [ApiController]
    public class BartenderController : ControllerBase
    {
        private readonly IBartenderService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BartenderController"/> class with the specified bartender service.
        /// </summary>
        /// <param name="service">The bartender service to use for data operations.</param>
        public BartenderController(IBartenderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all bartenders.
        /// </summary>
        /// <returns>A list of BartenderDto objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BartenderDto>>> GetBartenders()
        {
            var bartenders = await _service.GetAllAsync();
            return Ok(bartenders);
        }

        /// <summary>
        /// Retrieves a bartender by ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to retrieve.</param>
        /// <returns>The requested BartenderDto if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BartenderDto>> GetBartender(int id)
        {
            var bartender = await _service.GetByIdAsync(id);
            if (bartender == null) return NotFound();
            return Ok(bartender);
        }

        /// <summary>
        /// Creates a new bartender.
        /// </summary>
        /// <param name="dto">The BartenderDto object to create.</param>
        /// <returns>The created BartenderDto object with a location header.</returns>
        [HttpPost]
        public async Task<ActionResult<BartenderDto>> PostBartender(BartenderDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetBartender), new { id = result.BartenderId }, result);
        }

        /// <summary>
        /// Updates an existing bartender by ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to update.</param>
        /// <param name="dto">The updated BartenderDto object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBartender(int id, BartenderDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a bartender by ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to delete.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBartender(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
