using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Interface for Artist service operations such as retrieval, creation, updating, and deletion.
    /// </summary>
    public interface IArtistService
    {
        /// <summary>
        /// Retrieves a list of all artists asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a collection of <see cref="ArtistDTO"/>.</returns>
        Task<IEnumerable<ArtistDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves an artist by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The ID of the artist.</param>
        /// <returns>A task representing the asynchronous operation, containing the <see cref="ArtistDTO"/> if found; otherwise, null.</returns>
        Task<ArtistDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new artist asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing artist information.</param>
        /// <returns>A task representing the asynchronous operation, containing the newly created <see cref="ArtistDTO"/>.</returns>
        Task<ArtistDTO> CreateAsync(ArtistDTO dto);

        /// <summary>
        /// Updates an existing artist by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the artist to update.</param>
        /// <param name="dto">The updated artist information.</param>
        /// <returns>A task representing the asynchronous operation, containing true if update is successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, ArtistDTO dto);

        /// <summary>
        /// Deletes an artist by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>A task representing the asynchronous operation, containing true if deletion is successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
