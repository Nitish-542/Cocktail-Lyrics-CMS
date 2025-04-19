using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface defining operations for song-related actions.
    public interface ISongService
    {
        /// <summary>
        /// Retrieves a list of all songs.
        /// </summary>
        /// <returns>Task representing the asynchronous operation, with a collection of SongDTO objects.</returns>
        Task<IEnumerable<SongDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a single song by its ID.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <returns>Task representing the asynchronous operation, with the SongDTO object if found; otherwise, null.</returns>
        Task<SongDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new song entry.
        /// </summary>
        /// <param name="dto">Song data transfer object.</param>
        /// <returns>Task representing the asynchronous operation, with the created SongDTO object.</returns>
        Task<SongDTO> CreateAsync(SongDTO dto);

        /// <summary>
        /// Updates an existing song by ID.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <param name="dto">Updated SongDTO object.</param>
        /// <returns>Task representing the asynchronous operation, indicating success (true) or failure (false).</returns>
        Task<bool> UpdateAsync(int id, SongDTO dto);

        /// <summary>
        /// Deletes a song by ID.
        /// </summary>
        /// <param name="id">Song ID.</param>
        /// <returns>Task representing the asynchronous operation, indicating success (true) or failure (false).</returns>
        Task<bool> DeleteAsync(int id);
    }
}
