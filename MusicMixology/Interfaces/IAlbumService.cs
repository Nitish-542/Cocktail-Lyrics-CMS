using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Defines operations for managing album data within the MusicMixology application.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// Retrieves all albums asynchronously.
        /// </summary>
        /// <returns>A collection of AlbumDTOs.</returns>
        Task<IEnumerable<AlbumDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific album by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the album.</param>
        /// <returns>An AlbumDTO if found; otherwise, null.</returns>
        Task<AlbumDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new album entry.
        /// </summary>
        /// <param name="dto">The AlbumDTO object containing album data to be created.</param>
        /// <returns>The created AlbumDTO.</returns>
        Task<AlbumDTO> CreateAsync(AlbumDTO dto);

        /// <summary>
        /// Updates an existing album entry.
        /// </summary>
        /// <param name="id">The ID of the album to update.</param>
        /// <param name="dto">The AlbumDTO containing updated album data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, AlbumDTO dto);

        /// <summary>
        /// Deletes an album by its ID.
        /// </summary>
        /// <param name="id">The ID of the album to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
