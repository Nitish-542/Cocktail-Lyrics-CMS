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
    // Controller responsible for handling song-related actions.
    public class SongPageController : Controller
    {
        private readonly ISongService _songService;
        private readonly ApplicationDbContext _context;

        // Constructor that injects the song service and application DB context.
        public SongPageController(ISongService songService, ApplicationDbContext context)
        {
            _songService = songService;
            _context = context;
        }

        /// <summary>
        /// Displays a list of all songs.
        /// </summary>
        /// <returns>View displaying all songs.</returns>
        public async Task<IActionResult> Index()
        {
            var songs = await _songService.GetAllAsync();
            return View(songs);
        }

        /// <summary>
        /// Displays details of a specific song by its ID.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>View with song details if found, otherwise NotFound.</returns>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        /// <summary>
        /// Admin-only action to display the song creation form.
        /// </summary>
        /// <returns>View with song creation form.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = await BuildSongViewModelAsync();
            return View(vm);
        }

        /// <summary>
        /// Admin-only action to handle song creation via POST.
        /// </summary>
        /// <param name="vm">Song ViewModel</param>
        /// <returns>Redirect to the Index if successful; otherwise, redisplay form.</returns>
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
        /// Admin-only action to display the song edit form.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>View with song edit form.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = await BuildSongViewModelAsync(dto);
            return View(vm);
        }

        /// <summary>
        /// Admin-only action to handle song updates via POST.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <param name="vm">Updated Song ViewModel</param>
        /// <returns>Redirect to Index on success, or show error if failure.</returns>
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
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                await LoadDropdownsAsync(vm);
                return View(vm);
            }
        }

        /// <summary>
        /// Admin-only action to display the song delete confirmation form.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>View with delete confirmation.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _songService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Admin-only action to handle song deletion via POST.
        /// </summary>
        /// <param name="id">Song ID</param>
        /// <returns>Redirect to Index if deletion is successful, otherwise NotFound.</returns>
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
        /// Builds the SongViewModel from a SongDTO or returns an empty one.
        /// </summary>
        /// <param name="dto">SongDTO (optional)</param>
        /// <returns>SongViewModel populated with necessary data.</returns>
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
        /// Populates the dropdown lists for artists and albums.
        /// </summary>
        /// <param name="vm">SongViewModel to populate.</param>
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
