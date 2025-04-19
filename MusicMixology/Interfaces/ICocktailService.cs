using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface for defining operations related to cocktails.
    public interface ICocktailService
    {
        /// <summary>
        /// Retrieves all cocktails.
        /// </summary>
        /// <returns>List of CocktailDTO objects.</returns>
        Task<IEnumerable<CocktailDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific cocktail by its ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <returns>CocktailDTO object if found; otherwise, null.</returns>
        Task<CocktailDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new cocktail.
        /// </summary>
        /// <param name="dto">Cocktail data transfer object.</param>
        /// <returns>Created CocktailDTO object.</returns>
        Task<CocktailDTO> CreateAsync(CocktailDTO dto);

        /// <summary>
        /// Updates an existing cocktail by ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <param name="dto">Updated CocktailDTO object.</param>
        /// <returns>True if successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, CocktailDTO dto);

        /// <summary>
        /// Deletes a cocktail by its ID.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <returns>True if successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
