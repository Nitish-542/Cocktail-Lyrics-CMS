using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArtistDTO>> GetAllAsync()
        {
            return await _context.Artists
                .Select(a => new ArtistDTO
                {
                    ArtistId = a.ArtistId,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public async Task<ArtistDTO?> GetByIdAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return null;

            return new ArtistDTO
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name
            };
        }

        public async Task<ArtistDTO> CreateAsync(ArtistDTO dto)
        {
            var artist = new Artist { Name = dto.Name };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            dto.ArtistId = artist.ArtistId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, ArtistDTO dto)
        {
            if (id != dto.ArtistId) return false;

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return false;

            artist.Name = dto.Name;
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return false;

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
