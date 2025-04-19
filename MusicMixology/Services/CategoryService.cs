using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for managing cocktail category operations.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor for injecting the application's database context.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all cocktail categories.
        /// </summary>
        /// <returns>A list of CategoryDTO objects.</returns>
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific cocktail category by ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The CategoryDTO object if found; otherwise, null.</returns>
        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return null;

            return new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        /// <summary>
        /// Creates a new cocktail category.
        /// </summary>
        /// <param name="dto">The CategoryDTO object to create.</param>
        /// <returns>The created CategoryDTO with its new ID.</returns>
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

        /// <summary>
        /// Updates an existing cocktail category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="dto">The updated CategoryDTO object.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Deletes a cocktail category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
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
