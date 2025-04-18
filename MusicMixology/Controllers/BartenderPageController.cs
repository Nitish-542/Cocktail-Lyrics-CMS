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

        // GET: BartenderPage
        public async Task<IActionResult> Index()
        {
            var bartenders = await _bartenderService.GetAllAsync();
            return View(bartenders);
        }

        // GET: BartenderPage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BartenderPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BartenderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _bartenderService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: BartenderPage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: BartenderPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BartenderDto dto)
        {
            if (id != dto.BartenderId) return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            var updated = await _bartenderService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: BartenderPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: BartenderPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _bartenderService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: BartenderPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _bartenderService.GetDetailsWithCocktailsAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

    }
}
