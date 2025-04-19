using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for handling cocktail-related operations.
    /// Implements the ICocktailService interface.
    /// </summary>
    public class CocktailService : ICocktailService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor with dependency injection for ApplicationDbContext.
        /// </summary>
        public CocktailService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all cocktails, including related Bartender and Category data.
        /// </summary>
        /// <returns>List of CocktailDTOs.</returns>
        public async Task<IEnumerable<CocktailDTO>> GetAllAsync()
        {
            return await _context.Cocktails
                .Include(c => c.Bartender)
                .Include(c => c.Category)
                .Select(c => c.ToDto())
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single cocktail by its ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <returns>CocktailDTO if found; otherwise, null.</returns>
        public async Task<CocktailDTO?> GetByIdAsync(int id)
        {
            var cocktail = await _context.Cocktails
                .Include(c => c.Bartender)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.CocktailID == id);

            return cocktail?.ToDto();
        }

        /// <summary>
        /// Creates a new cocktail entry in the database.
        /// </summary>
        /// <param name="dto">CocktailDTO containing new cocktail data.</param>
        /// <returns>Created CocktailDTO with generated ID.</returns>
        public async Task<CocktailDTO> CreateAsync(CocktailDTO dto)
        {
            var cocktail = dto.ToEntity();

            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();

            dto.CocktailID = cocktail.CocktailID;
            return dto;
        }

        /// <summary>
        /// Updates an existing cocktail by ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <param name="dto">Updated CocktailDTO object.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Deletes a cocktail by ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var cocktail = await _context.Cocktails.FindAsync(id);
            if (cocktail == null) return false;

            _context.Cocktails.Remove(cocktail);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    /// <summary>
    /// Provides extension methods to map between Cocktail and CocktailDTO.
    /// </summary>
    public static class CocktailMapper
    {
        /// <summary>
        /// Maps a Cocktail entity to a CocktailDTO.
        /// </summary>
        /// <param name="c">Cocktail entity.</param>
        /// <returns>CocktailDTO with populated fields.</returns>
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

        /// <summary>
        /// Maps a CocktailDTO to a Cocktail entity.
        /// </summary>
        /// <param name="dto">CocktailDTO object.</param>
        /// <returns>Cocktail entity for database operations.</returns>
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
