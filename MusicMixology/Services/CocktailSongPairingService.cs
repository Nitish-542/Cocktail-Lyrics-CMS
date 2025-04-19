using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service for managing cocktail-song pairings.
    /// Implements operations to create, read, update, and delete pairings.
    /// </summary>
    public class CocktailSongPairingService : ICocktailSongPairingService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor injecting the application's DbContext.
        /// </summary>
        public CocktailSongPairingService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all cocktail-song pairings from the database.
        /// </summary>
        /// <returns>A list of CocktailSongPairingDTO objects.</returns>
        public async Task<IEnumerable<CocktailSongPairingDTO>> GetAllAsync()
        {
            return await _context.CocktailSongPairings
                .Include(p => p.Cocktail)
                .Include(p => p.Song)
                .Select(p => new CocktailSongPairingDTO
                {
                    PairingId = p.PairingId,
                    CocktailId = p.CocktailId,
                    Name = p.Cocktail.Name,
                    Cocktail = new CocktailDTO
                    {
                        CocktailID = p.Cocktail.CocktailID,
                        Name = p.Cocktail.Name,
                        Recipe = p.Cocktail.Recipe,
                        LiqIns = p.Cocktail.LiqIns,
                        MixIns = p.Cocktail.MixIns,
                        BartenderId = p.Cocktail.BartenderID,
                        CategoryId = p.Cocktail.CategoryID
                    },
                    SongId = p.SongId,
                    Title = p.Song.Title,
                    Song = new SongDTO
                    {
                        SongId = p.Song.SongId,
                        Title = p.Song.Title,
                        Genre = p.Song.Genre,
                        ArtistId = p.Song.ArtistId,
                        AlbumId = p.Song.AlbumId  
                    },
                    MoodCategory = p.MoodCategory
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific cocktail-song pairing by its ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to retrieve.</param>
        /// <returns>The pairing DTO if found; otherwise, null.</returns>
        public async Task<CocktailSongPairingDTO?> GetByIdAsync(int id)
        {
            var p = await _context.CocktailSongPairings
                .Include(p => p.Cocktail)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(p => p.PairingId == id);

            if (p == null) return null;

            return new CocktailSongPairingDTO
            {
                PairingId = p.PairingId,
                CocktailId = p.CocktailId,
                Name = p.Cocktail.Name,
                Cocktail = new CocktailDTO
                {
                    CocktailID = p.Cocktail.CocktailID,
                    Name = p.Cocktail.Name,
                    Recipe = p.Cocktail.Recipe,
                    LiqIns = p.Cocktail.LiqIns,
                    MixIns = p.Cocktail.MixIns,
                    BartenderId = (int)p.Cocktail.BartenderID,
                    CategoryId = (int)p.Cocktail.CategoryID
                },
                SongId = p.SongId,
                Title = p.Song.Title,
                Song = new SongDTO
                {
                    SongId = p.Song.SongId,
                    Title = p.Song.Title,
                    Genre = p.Song.Genre,
                    ArtistId = p.Song.ArtistId,
                    AlbumId = p.Song.AlbumId 
                },
                MoodCategory = p.MoodCategory
            };
        }

        /// <summary>
        /// Creates a new cocktail-song pairing.
        /// </summary>
        /// <param name="dto">The DTO representing the new pairing.</param>
        /// <returns>The created DTO with its assigned ID.</returns>
        public async Task<CocktailSongPairingDTO> CreateAsync(CocktailSongPairingDTO dto)
        {
            var pairing = new CocktailSongPairing
            {
                CocktailId = dto.CocktailId,
                SongId = dto.SongId,
                MoodCategory = dto.MoodCategory
            };

            _context.CocktailSongPairings.Add(pairing);
            await _context.SaveChangesAsync();

            dto.PairingId = pairing.PairingId;
            return dto;
        }

        /// <summary>
        /// Updates an existing cocktail-song pairing.
        /// </summary>
        /// <param name="id">The ID of the pairing to update.</param>
        /// <param name="dto">The updated DTO.</param>
        /// <returns>True if updated successfully; false if not found or ID mismatch.</returns>
        public async Task<bool> UpdateAsync(int id, CocktailSongPairingDTO dto)
        {
            if (id != dto.PairingId) return false;

            var pairing = await _context.CocktailSongPairings.FindAsync(id);
            if (pairing == null) return false;

            pairing.CocktailId = dto.CocktailId;
            pairing.SongId = dto.SongId;
            pairing.MoodCategory = dto.MoodCategory;

            _context.Entry(pairing).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Deletes a cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to delete.</param>
        /// <returns>True if deleted; false if not found.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var pairing = await _context.CocktailSongPairings.FindAsync(id);
            if (pairing == null) return false;

            _context.CocktailSongPairings.Remove(pairing);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
