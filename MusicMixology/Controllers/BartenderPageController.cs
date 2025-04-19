using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    public class BartenderPageController : Controller
    {
        private readonly IBartenderService _bartenderService;

        public BartenderPageController(IBartenderService bartenderService)
        {
            _bartenderService = bartenderService;
        }

        // ✅ Public access
        public async Task<IActionResult> Index()
        {
            var bartenders = await _bartenderService.GetAllAsync();
            return View(bartenders);
        }

        // ✅ Public access
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _bartenderService.GetDetailsWithCocktailsAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        // 🔐 Admin only
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // 🔐 Admin only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(BartenderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _bartenderService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // 🔐 Admin only
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // 🔐 Admin only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BartenderDto dto)
        {
            if (id != dto.BartenderId) return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            var updated = await _bartenderService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // 🔐 Admin only
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // 🔐 Admin only
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _bartenderService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
