using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDTO>> GetAllAsync();
        Task<ArtistDTO?> GetByIdAsync(int id);
        Task<ArtistDTO> CreateAsync(ArtistDTO dto);
        Task<bool> UpdateAsync(int id, ArtistDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
