using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface that defines the contract for album-related services.
    public interface IAlbumService
    {
        /// <summary>
        /// Retrieves a list of all albums.
        /// </summary>
        /// <returns>A collection of AlbumDTO objects.</returns>
        Task<IEnumerable<AlbumDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a single album by its ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>AlbumDTO object if found; otherwise, null.</returns>
        Task<AlbumDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new album.
        /// </summary>
        /// <param name="dto">Album data transfer object containing the album information.</param>
        /// <returns>The created AlbumDTO object.</returns>
        Task<AlbumDTO> CreateAsync(AlbumDTO dto);

        /// <summary>
        /// Updates an existing album by its ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <param name="dto">Updated AlbumDTO object with new values.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, AlbumDTO dto);

        /// <summary>
        /// Deletes an album by its ID.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
