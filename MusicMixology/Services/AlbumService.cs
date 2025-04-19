using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for handling album-related data operations.
    /// Implements the IAlbumService interface.
    /// </summary>
    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor with dependency injection of the application DB context.
        /// </summary>
        /// <param name="context">Application database context</param>
        public AlbumService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all albums along with associated artists and songs.
        /// </summary>
        /// <returns>A list of AlbumDTOs</returns>
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

        /// <summary>
        /// Retrieves a single album by its ID with related artist and songs.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>AlbumDTO if found; otherwise, null</returns>
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

        /// <summary>
        /// Creates a new album entry in the database.
        /// </summary>
        /// <param name="dto">AlbumDTO containing album details</param>
        /// <returns>The created AlbumDTO with the generated AlbumId</returns>
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

        /// <summary>
        /// Updates an existing album based on the given ID and DTO.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <param name="dto">Updated AlbumDTO</param>
        /// <returns>True if update is successful; otherwise, false</returns>
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

        /// <summary>
        /// Deletes an album from the database by its ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>True if deletion is successful; otherwise, false</returns>
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
