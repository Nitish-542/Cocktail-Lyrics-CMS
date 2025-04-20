using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Interface for cocktail-related service operations.
    /// Provides methods for retrieving, creating, updating, and deleting cocktail data.
    /// </summary>
    public interface ICocktailService
    {
        /// <summary>
        /// Retrieves all cocktails.
        /// </summary>
        /// <returns>A collection of CocktailDTOs.</returns>
        Task<IEnumerable<CocktailDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific cocktail by its ID.
        /// </summary>
        /// <param name="id">The ID of the cocktail.</param>
        /// <returns>The corresponding CocktailDTO, or null if not found.</returns>
        Task<CocktailDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new cocktail.
        /// </summary>
        /// <param name="dto">The cocktail data to create.</param>
        /// <returns>The created CocktailDTO.</returns>
        Task<CocktailDTO> CreateAsync(CocktailDTO dto);

        /// <summary>
        /// Updates an existing cocktail by ID.
        /// </summary>
        /// <param name="id">The ID of the cocktail to update.</param>
        /// <param name="dto">The updated cocktail data.</param>
        /// <returns>True if update is successful, false otherwise.</returns>
        Task<bool> UpdateAsync(int id, CocktailDTO dto);

        /// <summary>
        /// Deletes a cocktail by ID.
        /// </summary>
        /// <param name="id">The ID of the cocktail to delete.</param>
        /// <returns>True if deletion is successful, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
