using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Interfaces
{
    // Interface defining operations related to bartender management.
    public interface IBartenderService
    {
        /// <summary>
        /// Retrieves a list of all bartenders.
        /// </summary>
        /// <returns>Collection of BartenderDto objects.</returns>
        Task<IEnumerable<BartenderDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a single bartender by ID.
        /// </summary>
        /// <param name="id">Bartender ID</param>
        /// <returns>BartenderDto object if found; otherwise, null.</returns>
        Task<BartenderDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new bartender.
        /// </summary>
        /// <param name="dto">Bartender data transfer object.</param>
        /// <returns>Created BartenderDto object.</returns>
        Task<BartenderDto> CreateAsync(BartenderDto dto);

        /// <summary>
        /// Updates an existing bartender by ID.
        /// </summary>
        /// <param name="id">Bartender ID</param>
        /// <param name="dto">Updated BartenderDto object.</param>
        /// <returns>True if the update is successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, BartenderDto dto);

        /// <summary>
        /// Deletes a bartender by ID.
        /// </summary>
        /// <param name="id">Bartender ID</param>
        /// <returns>True if the deletion is successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves bartender details along with the cocktails they serve.
        /// </summary>
        /// <param name="id">Bartender ID</param>
        /// <returns>BartenderViewModel with details and cocktails if found; otherwise, null.</returns>
        Task<BartenderViewModel?> GetDetailsWithCocktailsAsync(int id);
    }
}
