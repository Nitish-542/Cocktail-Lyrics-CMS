using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    public class SongPageController : Controller
    {
        private readonly ISongService _songService;
        private readonly ApplicationDbContext _context;

        public SongPageController(ISongService songService, ApplicationDbContext context)
        {
            _songService = songService;
            _context = context;
        }

        // GET: SongPage
        public async Task<IActionResult> Index()
        {
            var songs = await _songService.GetAllAsync();
            return View(songs);
        }

        // GET: SongPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        // GET: SongPage/Create
        public async Task<IActionResult> Create()
        {
            var vm = await BuildSongViewModelAsync();
            return View(vm);
        }

        // POST: SongPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(vm);
                return View(vm);
            }

            var dto = new SongDTO
            {
                Title = vm.Title,
                Genre = vm.Genre,
                ArtistId = (int)vm.ArtistId,
                AlbumId = (int)vm.AlbumId
            };

            await _songService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: SongPage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = await BuildSongViewModelAsync(dto);
            return View(vm);
        }

        // POST: SongPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SongViewModel vm)
        {
            if (id != vm.SongId) return NotFound();

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(vm);
                return View(vm);
            }

            try
            {
                var dto = new SongDTO
                {
                    SongId = vm.SongId,
                    Title = vm.Title,
                    Genre = vm.Genre,
                    ArtistId = (int)vm.ArtistId,
                    AlbumId = (int)vm.AlbumId
                };

                var updated = await _songService.UpdateAsync(id, dto);
                if (!updated) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                await LoadDropdownsAsync(vm);
                return View(vm);
            }
        }

        // GET: SongPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: SongPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _songService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }


        // Helper: Build full SongViewModel from DTO
        private async Task<SongViewModel> BuildSongViewModelAsync(SongDTO? dto = null)
        {
            return new SongViewModel
            {
                SongId = dto?.SongId ?? 0,
                Title = dto?.Title ?? "",
                Genre = dto?.Genre ?? "",
                ArtistId = dto?.ArtistId ?? 0,
                AlbumId = dto?.AlbumId ?? 0,
                ArtistList = await _context.Artists
                    .Select(a => new SelectListItem { Value = a.ArtistId.ToString(), Text = a.Name })
                    .ToListAsync(),
                AlbumList = await _context.Albums
                    .Select(a => new SelectListItem { Value = a.AlbumId.ToString(), Text = a.AlbumTitle })
                    .ToListAsync()
            };
        }

        // Helper: Populate dropdowns for failed validation return
        private async Task LoadDropdownsAsync(SongViewModel vm)
        {
            vm.ArtistList = await _context.Artists
                .Select(a => new SelectListItem { Value = a.ArtistId.ToString(), Text = a.Name })
                .ToListAsync();

            vm.AlbumList = await _context.Albums
                .Select(a => new SelectListItem { Value = a.AlbumId.ToString(), Text = a.AlbumTitle })
                .ToListAsync();
        }
    }
}
