using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Services
{
    public class BartenderService : IBartenderService
    {
        private readonly ApplicationDbContext _context;

        public BartenderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BartenderDto>> GetAllAsync()
        {
            return await _context.Bartenders
                .Include(b => b.Cocktails)
                .Select(b => new BartenderDto
                {
                    BartenderId = b.BartenderId,
                    Name = b.Name,
                    Cocktails = b.Cocktails.Select(c => new CocktailDTO
                    {
                        CocktailID = c.CocktailID,
                        Name = c.Name,
                        Recipe = c.Recipe
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<BartenderDto?> GetByIdAsync(int id)
        {
            var b = await _context.Bartenders.FindAsync(id);
            if (b == null) return null;

            return new BartenderDto
            {
                BartenderId = b.BartenderId,
                Name = b.Name
            };
        }

        public async Task<BartenderDto> CreateAsync(BartenderDto dto)
        {
            var bartender = new Bartender { Name = dto.Name };
            _context.Bartenders.Add(bartender);
            await _context.SaveChangesAsync();

            dto.BartenderId = bartender.BartenderId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, BartenderDto dto)
        {
            if (id != dto.BartenderId)
                return false;

            var bartender = await _context.Bartenders.FindAsync(id);
            if (bartender == null)
                return false;

            bartender.Name = dto.Name;
            _context.Entry(bartender).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bartender = await _context.Bartenders.FindAsync(id);
            if (bartender == null)
                return false;

            _context.Bartenders.Remove(bartender);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<BartenderViewModel?> GetDetailsWithCocktailsAsync(int id)
        {
            var bartender = await _context.Bartenders
                .Include(b => b.Cocktails)
                .FirstOrDefaultAsync(b => b.BartenderId == id);

            if (bartender == null) return null;

            return new BartenderViewModel
            {
                BartenderId = bartender.BartenderId,
                Name = bartender.Name,
                Cocktails = bartender.Cocktails.ToList()
            };
        }
    }
}
