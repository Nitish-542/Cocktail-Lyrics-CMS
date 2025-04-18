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

        // GET: PairingPage
        public async Task<IActionResult> Index()
        {
            var pairings = await _pairingService.GetAllAsync();
            return View(pairings);
        }

        // GET: PairingPage/Details/5
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

        // GET: PairingPage/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PairingViewModel
            {
                CocktailList = await GetCocktailDropdown(),
                SongList = await GetSongDropdown()
            };
            return View(vm);
        }

        // POST: PairingPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: PairingPage/Edit/5
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

        // POST: PairingPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: PairingPage/Delete/5
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

        // POST: PairingPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _pairingService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // 🔧 Dropdown Helpers
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
