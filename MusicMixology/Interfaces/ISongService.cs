using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Defines contract for song-related operations.
    /// </summary>
    public interface ISongService
    {
        /// <summary>
        /// Retrieves all songs.
        /// </summary>
        /// <returns>A collection of SongDTO objects.</returns>
        Task<IEnumerable<SongDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a single song by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the song to retrieve.</param>
        /// <returns>The SongDTO if found; otherwise, null.</returns>
        Task<SongDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new song.
        /// </summary>
        /// <param name="dto">The song data transfer object containing song details.</param>
        /// <returns>The created SongDTO.</returns>
        Task<SongDTO> CreateAsync(SongDTO dto);

        /// <summary>
        /// Updates an existing song.
        /// </summary>
        /// <param name="id">The ID of the song to update.</param>
        /// <param name="dto">The updated song data.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, SongDTO dto);

        /// <summary>
        /// Deletes a song by its ID.
        /// </summary>
        /// <param name="id">The ID of the song to delete.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
