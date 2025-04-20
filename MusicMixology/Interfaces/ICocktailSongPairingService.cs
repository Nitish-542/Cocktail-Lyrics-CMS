using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    public interface ICocktailSongPairingService
    {
        Task<IEnumerable<CocktailSongPairingDTO>> GetAllAsync();
        Task<CocktailSongPairingDTO?> GetByIdAsync(int id);
        Task<CocktailSongPairingDTO> CreateAsync(CocktailSongPairingDTO dto);
        Task<bool> UpdateAsync(int id, CocktailSongPairingDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
