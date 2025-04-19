using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    public class PairingPageController : Controller
    {
        private readonly ICocktailSongPairingService _pairingService;
        private readonly ApplicationDbContext _context;

        public PairingPageController(ICocktailSongPairingService pairingService, ApplicationDbContext context)
        {
            _pairingService = pairingService;
            _context = context;
        }

        // ✅ Public: View all pairings + search
        public async Task<IActionResult> Index(string searchTerm)
        {
            var pairings = await _pairingService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                pairings = pairings
                    .Where(p =>
                        (p.Name?.ToLower().Contains(searchTerm) ?? false) ||
                        (p.Title?.ToLower().Contains(searchTerm) ?? false) ||
                        (p.MoodCategory?.ToLower().Contains(searchTerm) ?? false))
                    .ToList();
            }

            return View(pairings);
        }

        // ✅ Public: View pairing details
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _pairingService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new PairingViewModel
            {
                PairingId = dto.PairingId,
                CocktailId = dto.CocktailId,
                SongId = dto.SongId,
                MoodCategory = dto.MoodCategory,
                CocktailName = dto.Name,
                SongTitle = dto.Title
            };

            return View(vm);
        }

        // 🔐 Admin only: Create pairing
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = new PairingViewModel
            {
                CocktailList = await GetCocktailDropdown(),
                SongList = await GetSongDropdown()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PairingViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CocktailList = await GetCocktailDropdown(vm.CocktailId);
                vm.SongList = await GetSongDropdown(vm.SongId);
                return View(vm);
            }

            var dto = new CocktailSongPairingDTO
            {
                CocktailId = vm.CocktailId,
                SongId = vm.SongId,
                MoodCategory = vm.MoodCategory
            };

            await _pairingService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // 🔐 Admin only: Edit pairing
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _pairingService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new PairingViewModel
            {
                PairingId = dto.PairingId,
                CocktailId = dto.CocktailId,
                SongId = dto.SongId,
                MoodCategory = dto.MoodCategory,
                CocktailList = await GetCocktailDropdown(dto.CocktailId),
                SongList = await GetSongDropdown(dto.SongId)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, PairingViewModel vm)
        {
            if (id != vm.PairingId) return NotFound();

            if (!ModelState.IsValid)
            {
                vm.CocktailList = await GetCocktailDropdown(vm.CocktailId);
                vm.SongList = await GetSongDropdown(vm.SongId);
                return View(vm);
            }

            var dto = new CocktailSongPairingDTO
            {
                PairingId = vm.PairingId,
                CocktailId = vm.CocktailId,
                SongId = vm.SongId,
                MoodCategory = vm.MoodCategory
            };

            var updated = await _pairingService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // 🔐 Admin only: Delete pairing
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _pairingService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new PairingViewModel
            {
                PairingId = dto.PairingId,
                CocktailName = dto.Name,
                SongTitle = dto.Title,
                MoodCategory = dto.MoodCategory
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _pairingService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // 🔧 Dropdown helpers
        private async Task<List<SelectListItem>> GetCocktailDropdown(int? selectedId = null)
        {
            return await _context.Cocktails
                .Select(c => new SelectListItem
                {
                    Value = c.CocktailID.ToString(),
                    Text = c.Name,
                    Selected = c.CocktailID == selectedId
                }).ToListAsync();
        }

        private async Task<List<SelectListItem>> GetSongDropdown(int? selectedId = null)
        {
            return await _context.Songs
                .Select(s => new SelectListItem
                {
                    Value = s.SongId.ToString(),
                    Text = s.Title,
                    Selected = s.SongId == selectedId
                }).ToListAsync();
        }
    }
}
