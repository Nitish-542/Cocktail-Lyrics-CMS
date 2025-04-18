using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class CocktailService : ICocktailService
    {
        private readonly ApplicationDbContext _context;

        public CocktailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CocktailDTO>> GetAllAsync()
        {
            return await _context.Cocktails
                .Include(c => c.Bartender)
                .Include(c => c.Category)
                .Select(c => c.ToDto())
                .ToListAsync();
        }

        public async Task<CocktailDTO?> GetByIdAsync(int id)
        {
            var cocktail = await _context.Cocktails
                .Include(c => c.Bartender)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.CocktailID == id);

            return cocktail?.ToDto();
        }

        public async Task<CocktailDTO> CreateAsync(CocktailDTO dto)
        {
            var cocktail = dto.ToEntity();

            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();

            dto.CocktailID = cocktail.CocktailID;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CocktailDTO dto)
        {
            if (id != dto.CocktailID) return false;

            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null) return false;

            cocktail.Name = dto.Name;
            cocktail.Recipe = dto.Recipe;
            cocktail.LiqIns = dto.LiqIns;
            cocktail.MixIns = dto.MixIns;
            cocktail.CategoryID = dto.CategoryId;
            cocktail.BartenderID = dto.BartenderId;

            _context.Entry(cocktail).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null) return false;

            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    public static class CocktailMapper
    {
        public static CocktailDTO ToDto(this Cocktail c)
        {
            return new CocktailDTO
            {
                CocktailID = c.CocktailID,
                Name = c.Name,
                Recipe = c.Recipe,
                LiqIns = c.LiqIns,
                MixIns = c.MixIns,
                BartenderId = c.BartenderID,
                CategoryId = c.CategoryID,

                Bartender = c.Bartender != null ? new BartenderDto
                {
                    BartenderId = c.Bartender.BartenderId,
                    Name = c.Bartender.Name
                } : null,

                Category = c.Category != null ? new CategoryDTO
                {
                    CategoryId = c.Category.CategoryID,
                    CategoryName = c.Category.CategoryName
                } : null
            };
        }

        public static Cocktail ToEntity(this CocktailDTO dto)
        {
            return new Cocktail
            {
                CocktailID = dto.CocktailID,
                Name = dto.Name,
                Recipe = dto.Recipe,
                LiqIns = dto.LiqIns,
                MixIns = dto.MixIns,
                BartenderID = dto.BartenderId,
                CategoryID = dto.CategoryId
            };
        }
    }
}
