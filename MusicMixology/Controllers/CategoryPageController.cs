using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Controllers
{
    /// <summary>
    /// Controller for managing category pages.
    /// Handles displaying, creating, editing, and deleting categories.
    /// </summary>
    public class CategoryPageController : Controller
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor with dependency injection for the category service.
        /// </summary>
        public CategoryPageController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /// <summary>
        /// Public: Displays a list of all categories.
        /// </summary>
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var categories = await _categoryService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                categories = categories
                    .Where(c => c.CategoryName.ToLower().Contains(searchTerm))
                    .ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(categories);
        }

        /// <summary>
        /// Public: Displays details for a specific category.
        /// </summary>
        /// <param name="id">Category ID</param>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Admin only: Returns the create category form.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Admin only: Handles category creation.
        /// </summary>
        /// <param name="dto">Category data</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Admin only: Returns the edit category form.
        /// </summary>
        /// <param name="id">Category ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Admin only: Handles category editing.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="dto">Updated category data</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CategoryDTO dto)
        {
            if (id != dto.CategoryId) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            var updated = await _categoryService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Admin only: Returns the delete confirmation view.
        /// </summary>
        /// <param name="id">Category ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Admin only: Confirms and performs category deletion.
        /// </summary>
        /// <param name="id">Category ID</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
