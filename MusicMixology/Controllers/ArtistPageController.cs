using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    /// <summary>
    /// Controller for handling artist-related pages and operations.
    /// </summary>
    public class ArtistPageController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistPageController"/> class.
        /// </summary>
        public ArtistPageController(IArtistService artistService, ApplicationDbContext context)
        {
            _artistService = artistService;
            _context = context;
        }


        /// <summary>
        /// Displays a list of all artists with their albums and songs.
        /// </summary>
        /// <returns>A view containing a list of artists.</returns>
        public async Task<IActionResult> Index(string searchTerm)
        {
            var artists = await _context.Artists
                .Include(a => a.Albums)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Album)
                .ToListAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerSearch = searchTerm.ToLower();

                artists = artists.Where(a =>
                    (!string.IsNullOrEmpty(a.Name) && a.Name.ToLower().Contains(lowerSearch)) ||
                    a.Albums.Any(al => al.AlbumTitle.ToLower().Contains(lowerSearch)) ||
                    a.Songs.Any(s => s.Title.ToLower().Contains(lowerSearch))
                ).ToList();
            }

            var viewModels = artists.Select(a => new ArtistViewModel
            {
                ArtistId = a.ArtistId,
                Name = a.Name,
                Albums = a.Albums.Select(al => new AlbumDTO
                {
                    AlbumId = al.AlbumId,
                    AlbumTitle = al.AlbumTitle,
                    ArtistId = al.ArtistId
                }).ToList(),
                Songs = a.Songs.Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Genre = s.Genre,
                    AlbumId = s.AlbumId,
                    ArtistId = s.ArtistId,
                    AlbumTitle = s.Album?.AlbumTitle
                }).ToList()
            }).ToList();

            return View(viewModels);
        }

        /// <summary>
        /// Displays details for a specific artist.
        /// </summary>
        /// <param name="id">The ID of the artist.</param>
        /// <returns>A view containing artist details, or NotFound if not found.</returns>
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _artistService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var artist = await _context.Artists
                .Include(a => a.Albums)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Album)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            var vm = new ArtistViewModel
            {
                ArtistId = dto.ArtistId,
                Name = dto.Name,
                Albums = artist.Albums.Select(a => new AlbumDTO
                {
                    AlbumId = a.AlbumId,
                    AlbumTitle = a.AlbumTitle,
                    ArtistId = a.ArtistId
                }).ToList(),
                Songs = artist.Songs.Select(s => new SongDTO
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Genre = s.Genre,
                    AlbumId = s.AlbumId,
                    ArtistId = s.ArtistId,
                    AlbumTitle = s.Album?.AlbumTitle
                }).ToList()
            };

            return View(vm);
        }

        /// <summary>
        /// Displays the form for creating a new artist (Admin only).
        /// </summary>
        /// <returns>A view with an empty artist form.</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new ArtistViewModel());
        }

        /// <summary>
        /// Handles the POST request to create a new artist (Admin only).
        /// </summary>
        /// <param name="vm">The artist view model.</param>
        /// <returns>Redirects to Index if successful; otherwise, returns the form view with validation messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ArtistViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = new ArtistDTO { Name = vm.Name };
            await _artistService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the form to edit an existing artist (Admin only).
        /// </summary>
        /// <param name="id">The ID of the artist to edit.</param>
        /// <returns>A view pre-filled with artist data, or NotFound if not found.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _artistService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(new ArtistViewModel
            {
                ArtistId = dto.ArtistId,
                Name = dto.Name
            });
        }

        /// <summary>
        /// Handles the POST request to update an artist (Admin only).
        /// </summary>
        /// <param name="id">The ID of the artist being edited.</param>
        /// <param name="vm">The updated artist view model.</param>
        /// <returns>Redirects to Index if successful; otherwise, shows validation messages.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, ArtistViewModel vm)
        {
            if (id != vm.ArtistId) return NotFound();
            if (!ModelState.IsValid) return View(vm);

            var dto = new ArtistDTO
            {
                ArtistId = vm.ArtistId,
                Name = vm.Name
            };

            var updated = await _artistService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Displays the confirmation page for deleting an artist (Admin only).
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>A view showing artist details for confirmation.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _artistService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        /// <summary>
        /// Handles the confirmed delete operation (Admin only).
        /// </summary>
        /// <param name="id">The ID of the artist to delete.</param>
        /// <returns>Redirects to Index if successful; otherwise, NotFound.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _artistService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
