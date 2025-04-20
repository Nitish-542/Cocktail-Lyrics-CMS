using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;
using MusicMixology.Data;

namespace MusicMixology.Controllers
{
    public class CocktailPageController : Controller
    {
        private readonly ICocktailService _cocktailService;
        private readonly ApplicationDbContext _context; // still used for dropdowns

        public CocktailPageController(ICocktailService cocktailService, ApplicationDbContext context)
        {
            _cocktailService = cocktailService;
            _context = context;
        }

        // GET: CocktailPage/Index
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

        // GET: CocktailPage/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View(new CocktailDTO());
        }

        // POST: CocktailPage/Create
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

        // EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            await LoadDropdownsAsync(dto.CategoryId, dto.BartenderId);
            return View(dto);
        }

        // EDIT - POST
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

        // DELETE - GET (Confirm page)
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _cocktailService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // DETAILS - GET (optional)
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _cocktailService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }


        // Helper to load ViewBag dropdowns
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
