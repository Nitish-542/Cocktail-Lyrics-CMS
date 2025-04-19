using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    // Interface for category-related service operations.
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>List of CategoryDTO objects.</returns>
        Task<IEnumerable<CategoryDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>CategoryDTO object if found; otherwise, null.</returns>
        Task<CategoryDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="dto">Category data transfer object.</param>
        /// <returns>Created CategoryDTO object.</returns>
        Task<CategoryDTO> CreateAsync(CategoryDTO dto);

        /// <summary>
        /// Updates an existing category by ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="dto">Updated CategoryDTO object.</param>
        /// <returns>True if successful, otherwise false.</returns>
        Task<bool> UpdateAsync(int id, CategoryDTO dto);

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>True if successful, otherwise false.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
