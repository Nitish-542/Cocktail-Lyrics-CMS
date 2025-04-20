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
    /// <summary>
    /// MVC Controller for managing album views and CRUD operations via web pages.
    /// </summary>
    public class AlbumPageController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly ApplicationDbContext _context;

        public AlbumPageController(IAlbumService albumService, ApplicationDbContext context)
        {
            _albumService = albumService;
            _context = context;
        }

        // ✅ Anyone can view the album list (now supports search)
        public async Task<IActionResult> Index(string searchTerm)
        /// <summary>
        /// Displays a list of all albums. Public access.
        /// </summary>
        {
            var albums = await _albumService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                albums = albums
                    .Where(a =>
                        (!string.IsNullOrEmpty(a.AlbumTitle) && a.AlbumTitle.ToLower().Contains(searchTerm)) ||
                        (!string.IsNullOrEmpty(a.ArtistName) && a.ArtistName.ToLower().Contains(searchTerm)))
                    .ToList();
            }

            return View(albums);
        }


        /// <summary>
        /// Displays details for a specific album by ID. Public access.
        /// </summary>
        /// <param name="id">Album ID</param>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _albumService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }
        /// <summary>
        /// Displays album creation form. Admin-only access.
        /// </summary> ✅ Only Admins can access Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var vm = new AlbumViewModel
            {
                ArtistList = await GetArtistSelectListAsync()
            };
            return View(vm);
        }

        /// <summary>
        /// Handles album creation form submission. Admin-only access.
        /// </summary>
        /// <param name="vm">Album view model</param>

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(AlbumViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ArtistList = await GetArtistSelectListAsync();
                return View(vm);
            }

            var dto = new AlbumDTO
            {
                AlbumTitle = vm.AlbumTitle,
                ArtistId = vm.ArtistId
            };

            await _albumService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays album edit form. Admin-only access.
        /// </summary>
        /// <param name="id">Album ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _albumService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new AlbumViewModel
            {
                AlbumId = dto.AlbumId,
                AlbumTitle = dto.AlbumTitle,
                ArtistId = dto.ArtistId,
                ArtistList = await GetArtistSelectListAsync()
            };

            return View(vm);
        }

        /// <summary>
        /// Handles album edit form submission. Admin-only access.
        /// </summary>
        /// <param name="id">Album ID</param>
        /// <param name="vm">Updated album view model</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, AlbumViewModel vm)
        {
            if (id != vm.AlbumId) return NotFound();

            if (!ModelState.IsValid)
            {
                vm.ArtistList = await GetArtistSelectListAsync();
                return View(vm);
            }

            var dto = new AlbumDTO
            {
                AlbumId = vm.AlbumId,
                AlbumTitle = vm.AlbumTitle,
                ArtistId = vm.ArtistId
            };

            var updated = await _albumService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays album deletion confirmation page. Admin-only access.
        /// </summary>
        /// <param name="id">Album ID</param>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _albumService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Handles album deletion. Admin-only access.
        /// </summary>
        /// <param name="id">Album ID</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _albumService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Helper method to create a list of artists for dropdown selection in views.
        /// </summary>
        private async Task<List<SelectListItem>> GetArtistSelectListAsync()
        {
            return await _context.Artists
                .Select(a => new SelectListItem
                {
                    Value = a.ArtistId.ToString(),
                    Text = a.Name
                })
                .ToListAsync();
        }
    }
}
