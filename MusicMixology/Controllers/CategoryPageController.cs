using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Controllers
{
    public class CategoryPageController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryPageController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ✅ Public: View all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // ✅ Public: View details
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // 🔐 Admin only: Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

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

        // 🔐 Admin only: Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

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

        // 🔐 Admin only: Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

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
