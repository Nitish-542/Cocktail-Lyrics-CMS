using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Cocktails)
                .Select(c => new CategoryDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Cocktails = c.Cocktails.Select(co => new CocktailDTO
                    {
                        CocktailID = co.CocktailID,
                        Name = co.Name
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Cocktails)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null) return null;

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Cocktails = category.Cocktails.Select(c => new CocktailDTO
                {
                    CocktailID = c.CocktailID,
                    Name = c.Name,
                    Recipe = c.Recipe,
                    LiqIns = c.LiqIns,
                    MixIns = c.MixIns,
                    BartenderId = c.BartenderID,
                    CategoryId = c.CategoryID
                }).ToList()
            };
        }

        public async Task<CategoryDTO> CreateAsync(CategoryDTO dto)
        {
            var category = new CocktailCategory
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            dto.CategoryId = category.CategoryId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CategoryDTO dto)
        {
            if (id != dto.CategoryId)
                return false;

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            category.CategoryName = dto.CategoryName;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
