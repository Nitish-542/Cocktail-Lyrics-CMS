using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    /// <summary>
    /// Defines operations for managing music categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all music categories.
        /// </summary>
        /// <returns>A collection of CategoryDTO objects.</returns>
        Task<IEnumerable<CategoryDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a music category by its unique ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The matching CategoryDTO if found; otherwise, null.</returns>
        Task<CategoryDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new music category.
        /// </summary>
        /// <param name="dto">The category data to create.</param>
        /// <returns>The created CategoryDTO.</returns>
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);

        /// <summary>
        /// Updates an existing music category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="dto">The updated category data.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateAsync(int id, CategoryDTO dto);

        /// <summary>
        /// Deletes a music category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
