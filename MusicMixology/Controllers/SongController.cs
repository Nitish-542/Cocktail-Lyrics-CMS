using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    // API controller for managing song-related actions.
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _service;

        // Constructor with dependency injection for song service.
        public SongController(ISongService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all songs.
        /// </summary>
        /// <returns>List of SongDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _service.GetAllAsync();
            return Ok(songs);
        }

        /// <summary>
        /// Retrieves a single song by its ID.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>SongDTO object if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            var song = await _service.GetByIdAsync(id);
            if (song == null) return NotFound();
            return Ok(song);
        }

        /// <summary>
        /// Creates a new song.
        /// </summary>
        /// <param name="dto">Song data transfer object.</param>
        /// <returns>Created SongDTO object with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSong), new { id = result.SongId }, result);
        }

        /// <summary>
        /// Updates an existing song by ID.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <param name="dto">Updated SongDTO object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a song by ID.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
