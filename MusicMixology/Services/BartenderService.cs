using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for performing CRUD operations and queries related to bartenders.
    /// Implements the IBartenderService interface.
    /// </summary>
    public class BartenderService : IBartenderService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor that injects the application's database context.
        /// </summary>
        /// <param name="context">The application's database context.</param>
        public BartenderService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all bartenders from the database.
        /// </summary>
        /// <returns>A collection of BartenderDto objects.</returns>
        public async Task<IEnumerable<BartenderDto>> GetAllAsync()
        {
            return await _context.Bartenders
                .Select(b => new BartenderDto
                {
                    BartenderId = b.BartenderId,
                    Name = b.Name
                })
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a bartender by their ID.
        /// </summary>
        /// <param name="id">The ID of the bartender.</param>
        /// <returns>A BartenderDto if found, otherwise null.</returns>
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

        /// <summary>
        /// Creates a new bartender entry in the database.
        /// </summary>
        /// <param name="dto">The DTO containing bartender data.</param>
        /// <returns>The created BartenderDto with assigned ID.</returns>
        public async Task<BartenderDto> CreateAsync(BartenderDto dto)
        {
            var bartender = new Bartender { Name = dto.Name };
            _context.Bartenders.Add(bartender);
            await _context.SaveChangesAsync();

            dto.BartenderId = bartender.BartenderId;
            return dto;
        }

        /// <summary>
        /// Updates an existing bartender's information.
        /// </summary>
        /// <param name="id">The ID of the bartender to update.</param>
        /// <param name="dto">The updated BartenderDto object.</param>
        /// <returns>True if update is successful, otherwise false.</returns>
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

        /// <summary>
        /// Deletes a bartender by their ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to delete.</param>
        /// <returns>True if deletion is successful, otherwise false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var bartender = await _context.Bartenders.FindAsync(id);
            if (bartender == null)
                return false;

            _context.Bartenders.Remove(bartender);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Retrieves a bartender along with the list of cocktails they make.
        /// </summary>
        /// <param name="id">The ID of the bartender.</param>
        /// <returns>A BartenderViewModel with cocktail details if found, otherwise null.</returns>
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
