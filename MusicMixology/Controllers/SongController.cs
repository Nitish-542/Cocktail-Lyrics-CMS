using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    /// <summary>
    /// API controller for managing songs.
    /// Provides endpoints for CRUD operations on songs.
    /// </summary>
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SongController"/> class.
        /// </summary>
        /// <param name="service">Service for song-related operations.</param>
        public SongController(ISongService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets a list of all songs.
        /// </summary>
        /// <returns>A list of <see cref="SongDTO"/> objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _service.GetAllAsync();
            return Ok(songs);
        }

        /// <summary>
        /// Gets a specific song by its ID.
        /// </summary>
        /// <param name="id">The ID of the song.</param>
        /// <returns>The <see cref="SongDTO"/> if found, otherwise NotFound.</returns>
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
        /// <param name="dto">The song data to create.</param>
        /// <returns>The created <see cref="SongDTO"/> with status code 201.</returns>
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSong), new { id = result.SongId }, result);
        }

        /// <summary>
        /// Updates an existing song by ID.
        /// </summary>
        /// <param name="id">The ID of the song to update.</param>
        /// <param name="dto">The updated song data.</param>
        /// <returns>NoContent if successful, otherwise NotFound.</returns>
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
        /// <param name="id">The ID of the song to delete.</param>
        /// <returns>NoContent if successful, otherwise NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
