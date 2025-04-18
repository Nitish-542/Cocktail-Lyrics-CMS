using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class CocktailSongPairingService : ICocktailSongPairingService
    {
        private readonly ApplicationDbContext _context;

        public CocktailSongPairingService(ApplicationDbContext context)
        {
            _context = context;
        }

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
