using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<SongDTO>> GetAllAsync();
        Task<SongDTO?> GetByIdAsync(int id);
        Task<SongDTO> CreateAsync(SongDTO dto);
        Task<bool> UpdateAsync(int id, SongDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
