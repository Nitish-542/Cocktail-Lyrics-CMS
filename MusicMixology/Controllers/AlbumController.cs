using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    // API controller for managing album-related actions.
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;
        // Constructor with dependency injection for album service.
        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all albums.
        /// </summary>
        /// <returns>List of AlbumDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            var albums = await _service.GetAllAsync();
            return Ok(albums);
        }

        /// <summary>
        /// Retrieves a single album by its ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>AlbumDTO object if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = await _service.GetByIdAsync(id);
            if (album == null) return NotFound();
            return Ok(album);
        }

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="dto">Album data transfer object.</param>
        /// <returns>Created AlbumDTO object with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> PostAlbum(AlbumDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAlbum), new { id = result.AlbumId }, result);
        }

        /// <summary>
        /// Updates an existing album by ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <param name="dto">Updated AlbumDTO object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes an album by ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
