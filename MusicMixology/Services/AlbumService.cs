using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _context;

        public AlbumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlbumDTO>> GetAllAsync()
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Artist)
                .Select(a => new AlbumDTO
                {
                    AlbumId = a.AlbumId,
                    AlbumTitle = a.AlbumTitle,
                    ArtistId = a.ArtistId,
                    ArtistName = a.Artist.Name,
                    Songs = a.Songs.Select(s => new SongDTO
                    {
                        SongId = s.SongId,
                        Title = s.Title,
                        ArtistName = s.Artist.Name,
                        Genre = s.Genre
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<AlbumDTO?> GetByIdAsync(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Artist)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null) return null;

            return new AlbumDTO
            {
                AlbumId = album.AlbumId,
                AlbumTitle = album.AlbumTitle,
                ArtistId = album.ArtistId,
                ArtistName = album.Artist?.Name,
                Songs = album.Songs.Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Genre = s.Genre,
                    ArtistId = s.ArtistId,
                    AlbumId = s.AlbumId,
                    ArtistName = s.Artist?.Name
                }).ToList()
            };
        }



        public async Task<AlbumDTO> CreateAsync(AlbumDTO dto)
        {
            var album = new Album
            {
                AlbumTitle = dto.AlbumTitle,
                ArtistId = dto.ArtistId
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();

            dto.AlbumId = album.AlbumId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, AlbumDTO dto)
        {
            if (id != dto.AlbumId) return false;

            var album = await _context.Albums.FindAsync(id);
            if (album == null) return false;

            album.AlbumTitle = dto.AlbumTitle;
            album.ArtistId = dto.ArtistId;

            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null) return false;

            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
