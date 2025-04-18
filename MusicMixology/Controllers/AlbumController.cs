using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _service;

        public AlbumController(IAlbumService service)
        {
            _service = service;
        }

        // GET: api/albums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumDTO>>> GetAlbums()
        {
            var albums = await _service.GetAllAsync();
            return Ok(albums);
        }

        // GET: api/albums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlbumDTO>> GetAlbum(int id)
        {
            var album = await _service.GetByIdAsync(id);
            if (album == null) return NotFound();
            return Ok(album);
        }

        // POST: api/albums
        [HttpPost]
        public async Task<ActionResult<AlbumDTO>> PostAlbum(AlbumDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAlbum), new { id = result.AlbumId }, result);
        }

        // PUT: api/albums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/albums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
