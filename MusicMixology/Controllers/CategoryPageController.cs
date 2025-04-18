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

        // GET: CategoryPage
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // GET: CategoryPage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryPage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: CategoryPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryDTO dto)
        {
            if (id != dto.CategoryId) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            var updated = await _categoryService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // GET: CategoryPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: CategoryPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
