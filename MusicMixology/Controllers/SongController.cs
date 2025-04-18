using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongService _service;

        public SongController(ISongService service)
        {
            _service = service;
        }

        // GET: api/songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _service.GetAllAsync();
            return Ok(songs);
        }

        // GET: api/songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            var song = await _service.GetByIdAsync(id);
            if (song == null) return NotFound();
            return Ok(song);
        }

        // POST: api/songs
        [HttpPost]
        public async Task<ActionResult<SongDTO>> PostSong(SongDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSong), new { id = result.SongId }, result);
        }

        // PUT: api/songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // DELETE: api/songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
