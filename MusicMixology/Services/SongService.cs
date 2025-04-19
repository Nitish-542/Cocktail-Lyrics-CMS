using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for managing song-related operations.
    /// Implements the ISongService interface.
    /// </summary>
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor that injects the application's database context.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public SongService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all songs, including related artist and album data.
        /// </summary>
        /// <returns>List of SongDTO objects.</returns>
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

        /// <summary>
        /// Retrieves a specific song by ID, including artist and album information.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <returns>SongDTO object if found; otherwise, null.</returns>
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

        /// <summary>
        /// Creates a new song based on the provided DTO.
        /// </summary>
        /// <param name="dto">Song data transfer object.</param>
        /// <returns>The created SongDTO with assigned ID.</returns>
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

        /// <summary>
        /// Updates an existing song identified by ID using the provided DTO.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <param name="dto">Updated SongDTO object.</param>
        /// <returns>True if update is successful; otherwise, false.</returns>
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

        /// <summary>
        /// Deletes a song identified by ID.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <returns>True if deletion is successful; otherwise, false.</returns>
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
