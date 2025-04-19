using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface defining the contract for artist-related services.
    public interface IArtistService
    {
        /// <summary>
        /// Retrieves a list of all artists.
        /// </summary>
        /// <returns>List of ArtistDTO objects.</returns>
        Task<IEnumerable<ArtistDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves an artist by their ID.
        /// </summary>
        /// <param name="id">Artist ID</param>
        /// <returns>ArtistDTO object if found; otherwise, null.</returns>
        Task<ArtistDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new artist.
        /// </summary>
        /// <param name="dto">Artist data transfer object containing new artist information.</param>
        /// <returns>Created ArtistDTO object.</returns>
        Task<ArtistDTO> CreateAsync(ArtistDTO dto);

        /// <summary>
        /// Updates an existing artist's information.
        /// </summary>
        /// <param name="id">Artist ID</param>
        /// <param name="dto">Updated ArtistDTO object.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, ArtistDTO dto);

        /// <summary>
        /// Deletes an artist by their ID.
        /// </summary>
        /// <param name="id">Artist ID</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
