using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Defines operations related to Bartender management in the MusicMixology application.
    /// </summary>
    public interface IBartenderService
    {
        /// <summary>
        /// Retrieves a list of all bartenders.
        /// </summary>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result contains a list of BartenderDto objects.</returns>
        Task<IEnumerable<BartenderDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific bartender by their ID.
        /// </summary>
        /// <param name="id">The ID of the bartender.</param>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result contains a BartenderDto if found, otherwise null.</returns>
        Task<BartenderDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new bartender.
        /// </summary>
        /// <param name="dto">The bartender data transfer object containing the details to be created.</param>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result contains the newly created BartenderDto.</returns>
        Task<BartenderDto> CreateAsync(BartenderDto dto);

        /// <summary>
        /// Updates an existing bartender by ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to update.</param>
        /// <param name="dto">The updated bartender data transfer object.</param>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result indicates whether the update was successful.</returns>
        Task<bool> UpdateAsync(int id, BartenderDto dto);

        /// <summary>
        /// Deletes a bartender by ID.
        /// </summary>
        /// <param name="id">The ID of the bartender to delete.</param>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves bartender details along with their associated cocktails.
        /// </summary>
        /// <param name="id">The ID of the bartender.</param>
        /// <returns>A task that represents an asynchronous operation. 
        /// The task result contains a BartenderViewModel with related cocktail information if found, otherwise null.</returns>
        Task<BartenderViewModel?> GetDetailsWithCocktailsAsync(int id);
    }
}
