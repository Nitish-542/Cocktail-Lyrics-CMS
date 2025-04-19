using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;
using MusicMixology.Data;

namespace MusicMixology.Controllers
{
    /// <summary>
    /// MVC controller for handling cocktail listing, details, and admin CRUD operations.
    /// </summary>
    public class CocktailPageController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly ApplicationDbContext _context;

        public CocktailPageController(ICocktailService cocktailService, ApplicationDbContext context)
        {
            _cocktailService = cocktailService;
            _context = context;
        }

        /// <summary>
        /// Displays a list of cocktails with optional filtering by category and bartender.
        /// </summary>
        /// <param name="SelectedCategoryId">Optional filter for category ID</param>
        /// <param name="SelectedBartenderId">Optional filter for bartender ID</param>
        /// <returns>View with CocktailListViewModel</returns>
        public async Task<IActionResult> Index(int? SelectedCategoryId, int? SelectedBartenderId)
        {
            var cocktails = await _cocktailService.GetAllAsync();

            if (SelectedCategoryId.HasValue)
                cocktails = cocktails.Where(c => c.CategoryId == SelectedCategoryId).ToList();

            if (SelectedBartenderId.HasValue)
                cocktails = cocktails.Where(c => c.BartenderId == SelectedBartenderId).ToList();

            var vm = new CocktailListViewModel
            {
                Cocktails = cocktails,
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    }).ToListAsync(),
                Bartenders = await _context.Bartenders
                    .Select(b => new SelectListItem
                    {
                        Value = b.BartenderId.ToString(),
                        Text = b.Name
                    }).ToListAsync(),
                SelectedCategoryId = SelectedCategoryId,
                SelectedBartenderId = SelectedBartenderId
            };

            return View(vm);
        }

        /// <summary>
        /// Displays details of a specific cocktail.
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <returns>View with CocktailDTO if found; otherwise, NotFound</returns>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Displays the create cocktail form (Admin only).
        /// </summary>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new CocktailDTO());
        }

        /// <summary>
        /// Handles form submission for creating a new cocktail (Admin only).
        /// </summary>
        /// <param name="dto">Cocktail data</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CocktailDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
                return View(dto);
            }

            await _cocktailService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the edit cocktail form with pre-filled data (Admin only).
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
            return View(dto);
        }

        /// <summary>
        /// Handles form submission for editing a cocktail (Admin only).
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        /// <param name="dto">Updated cocktail data</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, CocktailDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
                return View(dto);
            }

            var updated = await _cocktailService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays confirmation page for deleting a cocktail (Admin only).
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Handles the deletion of a cocktail (Admin only).
        /// </summary>
        /// <param name="id">Cocktail ID</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _cocktailService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to populate dropdown lists for categories and bartenders.
        /// </summary>
        /// <param name="selectedCategoryId">Selected category (optional)</param>
        /// <param name="selectedBartenderId">Selected bartender (optional)</param>
        private async Task LoadDropdownsAsync(int? selectedCategoryId = null, int? selectedBartenderId = null)
        {
            ViewBag.Categories = new SelectList(
                await _context.Categories.ToListAsync(),
                "CategoryId", "CategoryName", selectedCategoryId
            );

            ViewBag.Bartenders = new SelectList(
                await _context.Bartenders.ToListAsync(),
                "BartenderId", "Name", selectedBartenderId
            );
        }
    }
}
