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
    /// Controller responsible for managing cocktails, including CRUD operations and filtering.
    /// </summary>
    public class CocktailPageController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly ApplicationDbContext _context; // Used for dropdown data

        public CocktailPageController(ICocktailService cocktailService, ApplicationDbContext context)
        {
            _cocktailService = cocktailService;
            _context = context;
        }

        /// <summary>
        /// Displays the list of cocktails with optional filters for category and bartender.
        /// </summary>
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
        /// Displays the Create Cocktail form with dropdown data.
        /// </summary>
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new CocktailDTO());
        }

        /// <summary>
        /// Handles POST request to create a new cocktail entry.
        /// Validates model state and saves using the cocktail service.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CocktailDTO dto)
        {
            Console.WriteLine("🔄 FORM SUBMITTED");
            Console.WriteLine($"Name: {dto.Name}, CategoryID: {dto.CategoryId}, BartenderID: {dto.BartenderId}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is invalid. Errors:");
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        Console.WriteLine($"→ {entry.Key}: {error.ErrorMessage}");
                    }
                }

                await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
                return View(dto);
            }

            await _cocktailService.CreateAsync(dto);
            Console.WriteLine("✅ Cocktail saved to DB via service");
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Loads the Edit form for a cocktail based on the ID.
        /// </summary>
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
            return View(dto);
        }

        /// <summary>
        /// Handles POST request to update a cocktail record.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        /// Displays the confirmation page before deleting a cocktail.
        /// </summary>
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Handles confirmed deletion of a cocktail.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _cocktailService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the details of a specific cocktail.
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Helper method to load dropdowns (categories and bartenders) into ViewBag.
        /// </summary>
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
