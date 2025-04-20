using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;

        public SongService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SongDTO>> GetAllAsync()
        {
            return await _context.Songs
        .Include(s => s.Artist)
        .Include(s => s.Album)
        .Select(s => new SongDTO
        {
            SongId = s.SongId,
            Title = s.Title,
            Genre = s.Genre,
            ArtistId = s.ArtistId,
            AlbumId = s.AlbumId,
            ArtistName = s.Artist != null ? s.Artist.Name : "Unknown",
            AlbumTitle = s.Album != null ? s.Album.AlbumTitle : null
        })
        .ToListAsync();
        }


        public async Task<SongDTO?> GetByIdAsync(int id)
        {
            var song = await _context.Songs
      .Include(s => s.Artist)
      .Include(s => s.Album)
      .FirstOrDefaultAsync(s => s.SongId == id);

            if (song == null) return null;

            return new SongDTO
            {
                SongId = song.SongId,
                Title = song.Title,
                Genre = song.Genre,
                ArtistId = song.ArtistId,
                AlbumId = song.AlbumId,
                ArtistName = song.Artist?.Name,
                AlbumTitle = song.Album?.AlbumTitle
            };
        }

        public async Task<SongDTO> CreateAsync(SongDTO dto)
        {
            var song = new Song
            {
                Title = dto.Title,
                Genre = dto.Genre,
                ArtistId = dto.ArtistId,
                AlbumId = dto.AlbumId
            };

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            dto.SongId = song.SongId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, SongDTO dto)
        {
            if (id != dto.SongId)
                return false;

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
                return false;

            song.Title = dto.Title;
            song.Genre = dto.Genre;
            song.ArtistId = dto.ArtistId;
            song.AlbumId = dto.AlbumId;

            _context.Entry(song).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
                return false;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
