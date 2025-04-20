using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.ApiControllers
{
    // API controller for managing category-related operations.
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        // Constructor with dependency injection for category service.
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all categories.
        /// </summary>
        /// <returns>List of CategoryDTO objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves a single category by its ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>CategoryDTO object if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="dto">Category data transfer object.</param>
        /// <returns>Created CategoryDTO object with location header.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCategory), new { id = created.CategoryId }, created);
        }

        /// <summary>
        /// Updates an existing category by ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="dto">Updated CategoryDTO object.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
