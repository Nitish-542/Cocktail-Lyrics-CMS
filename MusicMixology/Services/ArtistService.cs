using Microsoft.EntityFrameworkCore;
using MusicMixology.Data;
using MusicMixology.Interfaces;
using MusicMixology.Models;

namespace MusicMixology.Services
{
    /// <summary>
    /// Service class for managing Artist-related data operations.
    /// Implements IArtistService interface.
    /// </summary>
    public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor with dependency injection for the database context.
        /// </summary>
        /// <param name="context">ApplicationDbContext instance.</param>
        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all artists as a list of ArtistDTO.
        /// </summary>
        /// <returns>A list of ArtistDTO objects.</returns>
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

        /// <summary>
        /// Retrieves a single artist by ID.
        /// </summary>
        /// <param name="id">The ID of the artist.</param>
        /// <returns>An ArtistDTO object if found; otherwise, null.</returns>
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

        /// <summary>
        /// Creates a new artist and returns the created DTO with the generated ID.
        /// </summary>
        /// <param name="dto">ArtistDTO containing the new artist's data.</param>
        /// <returns>The created ArtistDTO with the assigned ID.</returns>
        public async Task<ArtistDTO> CreateAsync(ArtistDTO dto)
        {
            var artist = new Artist { Name = dto.Name };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            dto.ArtistId = artist.ArtistId;
            return dto;
        }

        /// <summary>
        /// Updates an existing artist's information.
        /// </summary>
        /// <param name="id">ID of the artist to update.</param>
        /// <param name="dto">ArtistDTO with updated data.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Deletes an artist by ID.
        /// </summary>
        /// <param name="id">ID of the artist to delete.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
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
