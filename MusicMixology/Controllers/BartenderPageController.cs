using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    /// <summary>
    /// Controller for handling bartender-related views and actions.
    /// </summary>
    public class BartenderPageController : Controller
    {
        private readonly IBartenderService _bartenderService;

        /// <summary>
        /// Constructor with dependency injection for the bartender service.
        /// </summary>
        /// <param name="bartenderService">Service to manage bartender data.</param>
        public BartenderPageController(IBartenderService bartenderService)
        {
            _bartenderService = bartenderService;
        }

        /// <summary>
        /// Displays a list of all bartenders.
        /// Public access.
        /// </summary>
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var bartenders = await _bartenderService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                bartenders = bartenders
                    .Where(b => b.Name.ToLower().Contains(searchTerm))
                    .ToList();
            }

            ViewBag.SearchTerm = searchTerm;
            return View(bartenders);
        }

        /// <summary>
        /// Displays detailed view of a specific bartender and their cocktails.
        /// Public access.
        /// </summary>
        /// <param name="id">Bartender ID.</param>
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _bartenderService.GetDetailsWithCocktailsAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        /// <summary>
        /// Shows the form to create a new bartender.
        /// Admin access only.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processes the creation of a new bartender.
        /// Admin access only.
        /// </summary>
        /// <param name="dto">Data for the new bartender.</param>
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

        /// <summary>
        /// Shows the form to edit an existing bartender.
        /// Admin access only.
        /// </summary>
        /// <param name="id">Bartender ID.</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Processes updates to an existing bartender.
        /// Admin access only.
        /// </summary>
        /// <param name="id">Bartender ID.</param>
        /// <param name="dto">Updated bartender data.</param>
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

        /// <summary>
        /// Shows confirmation page for deleting a bartender.
        /// Admin access only.
        /// </summary>
        /// <param name="id">Bartender ID.</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _bartenderService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Processes the deletion of a bartender.
        /// Admin access only.
        /// </summary>
        /// <param name="id">Bartender ID.</param>
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
