using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    public class ArtistPageController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ApplicationDbContext _context;

        public ArtistPageController(IArtistService artistService, ApplicationDbContext context)
        {
            _artistService = artistService;
            _context = context;
        }

        // ✅ Everyone can view the list of artists
        public async Task<IActionResult> Index()
        {
            var artists = await _context.Artists
                .Include(a => a.Albums)
                .Include(a => a.Songs)
                .ThenInclude(s => s.Album)
                .ToListAsync();

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

        // ✅ Everyone can view artist details
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

        // ✅ Admin-only: GET Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new ArtistViewModel());
        }

        // ✅ Admin-only: POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ArtistViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = new ArtistDTO
            {
                Name = vm.Name
            };

            await _artistService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✅ Admin-only: GET Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _artistService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var vm = new ArtistViewModel
            {
                ArtistId = dto.ArtistId,
                Name = dto.Name
            };

            return View(vm);
        }

        // ✅ Admin-only: POST Edit
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

        // ✅ Admin-only: GET Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _artistService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // ✅ Admin-only: POST Delete
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
