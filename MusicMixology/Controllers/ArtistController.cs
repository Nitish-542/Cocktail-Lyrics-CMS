using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    // API controller for managing artist-related operations.
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _service;

        // Constructor with dependency injection for artist service.
        public ArtistController(IArtistService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>List of ArtistDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            var artists = await _service.GetAllAsync();
            return Ok(artists);
        }

        /// <summary>
        /// Retrieves a single artist by ID.
        /// </summary>
        /// <param name="id">Artist ID.</param>
        /// <returns>ArtistDTO object if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            var artist = await _service.GetByIdAsync(id);
            if (artist == null) return NotFound();
            return Ok(artist);
        }

        /// <summary>
        /// Creates a new artist.
        /// </summary>
        /// <param name="dto">Artist data transfer object.</param>
        /// <returns>Created ArtistDTO object with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist(ArtistDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetArtist), new { id = created.ArtistId }, created);
        }

        /// <summary>
        /// Updates an existing artist by ID.
        /// </summary>
        /// <param name="id">Artist ID.</param>
        /// <param name="dto">Updated ArtistDTO object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes an artist by ID.
        /// </summary>
        /// <param name="id">Artist ID.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
