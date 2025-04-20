using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDTO>> GetAllAsync();
        Task<AlbumDTO?> GetByIdAsync(int id);
        Task<AlbumDTO> CreateAsync(AlbumDTO dto);
        Task<bool> UpdateAsync(int id, AlbumDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
