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
    // 🎵 Controller responsible for managing the Song pages (CRUD operations)
    public class SongPageController : Controller
    {
        private readonly ISongService _songService;
        private readonly ApplicationDbContext _context;

        // ✅ Constructor injecting the Song Service and DB context
        public SongPageController(ISongService songService, ApplicationDbContext context)
        {
            _songService = songService;
            _context = context;
        }

        /// <summary>
        /// Displays a list of all songs, supports optional search by title or genre.
        /// </summary>
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var songs = await _songService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                songs = songs.Where(s =>
                    s.Title.ToLower().Contains(searchTerm) ||
                    s.Genre.ToLower().Contains(searchTerm)
                ).ToList();
            }

            return View(songs);
        }

        /// <summary>
        /// Displays detailed view of a selected song.
        /// </summary>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        /// <summary>
        /// Admin-only: Loads the form to create a new song.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = await BuildSongViewModelAsync();
            return View(vm);
        }

        /// <summary>
        /// Admin-only: Handles POST request to create a new song.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Admin-only: Loads the form to edit an existing song.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = await BuildSongViewModelAsync(dto);
            return View(vm);
        }

        /// <summary>
        /// Admin-only: Handles POST request to update a song.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
                ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
                await LoadDropdownsAsync(vm);
                return View(vm);
            }
        }

        /// <summary>
        /// Admin-only: Loads confirmation view for deleting a song.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        /// <summary>
        /// Admin-only: Handles POST request to delete a song.
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _songService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper: Builds a SongViewModel with dropdown lists preloaded. Used in Create/Edit views.
        /// </summary>
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

        /// <summary>
        /// Helper: Loads dropdown data (Artists and Albums) into the SongViewModel.
        /// </summary>
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
