using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Defines contract for managing cocktail and song pairing operations.
    /// </summary>
    public interface ICocktailSongPairingService
    {
        /// <summary>
        /// Retrieves all cocktail-song pairings.
        /// </summary>
        /// <returns>A collection of CocktailSongPairingDTO objects.</returns>
        Task<IEnumerable<CocktailSongPairingDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to retrieve.</param>
        /// <returns>The CocktailSongPairingDTO object if found; otherwise, null.</returns>
        Task<CocktailSongPairingDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new cocktail-song pairing.
        /// </summary>
        /// <param name="dto">The pairing data to create.</param>
        /// <returns>The created CocktailSongPairingDTO object.</returns>
        Task<CocktailSongPairingDTO> CreateAsync(CocktailSongPairingDTO dto);

        /// <summary>
        /// Updates an existing cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to update.</param>
        /// <param name="dto">The updated pairing data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, CocktailSongPairingDTO dto);

        /// <summary>
        /// Deletes a cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">The ID of the pairing to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
