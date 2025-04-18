using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Controllers
{
    public class AlbumPageController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly ApplicationDbContext _context;

        public AlbumPageController(IAlbumService albumService, ApplicationDbContext context)
        {
            _albumService = albumService;
            _context = context;
        }

        // GET: AlbumPage
        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAllAsync();
            return View(albums);
        }

        // GET: AlbumPage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var dto = await _albumService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // GET: AlbumPage/Create
        public async Task<IActionResult> Create()
        {
            var vm = new AlbumViewModel
            {
                ArtistList = await GetArtistSelectListAsync()
            };
            return View(vm);
        }

        // POST: AlbumPage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: AlbumPage/Edit/5
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

        // POST: AlbumPage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: AlbumPage/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _albumService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        // POST: AlbumPage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _albumService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // 🔧 Helper to get artist dropdown list
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
