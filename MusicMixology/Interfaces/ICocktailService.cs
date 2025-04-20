using MusicMixology.Models;

namespace MusicMixology.Interfaces
{
    public interface ICocktailService
    {
        Task<IEnumerable<CocktailDTO>> GetAllAsync();
        Task<CocktailDTO?> GetByIdAsync(int id);
        Task<CocktailDTO> CreateAsync(CocktailDTO dto);
        Task<bool> UpdateAsync(int id, CocktailDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}