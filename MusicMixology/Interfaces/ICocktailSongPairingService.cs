using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface for the Cocktail-Song Pairing Service.
    public interface ICocktailSongPairingService
    {
        /// <summary>
        /// Retrieves all cocktail-song pairings.
        /// </summary>
        /// <returns>A collection of CocktailSongPairingDTO objects.</returns>
        Task<IEnumerable<CocktailSongPairingDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a cocktail-song pairing by its ID.
        /// </summary>
        /// <param name="id">ID of the cocktail-song pairing.</param>
        /// <returns>A CocktailSongPairingDTO object if found; otherwise, null.</returns>
        Task<CocktailSongPairingDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new cocktail-song pairing.
        /// </summary>
        /// <param name="dto">Data transfer object containing cocktail-song pairing details.</param>
        /// <returns>The created CocktailSongPairingDTO object.</returns>
        Task<CocktailSongPairingDTO> CreateAsync(CocktailSongPairingDTO dto);

        /// <summary>
        /// Updates an existing cocktail-song pairing by ID.
        /// </summary>
        /// <param name="id">ID of the cocktail-song pairing.</param>
        /// <param name="dto">Updated CocktailSongPairingDTO object.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, CocktailSongPairingDTO dto);

        /// <summary>
        /// Deletes a cocktail-song pairing by its ID.
        /// </summary>
        /// <param name="id">ID of the cocktail-song pairing to be deleted.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
