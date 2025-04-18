using MusicMixology.Models;
using MusicMixology.ViewModels;

namespace MusicMixology.Interfaces
{
    public interface IBartenderService
    {
        Task<IEnumerable<BartenderDto>> GetAllAsync();
        Task<BartenderDto?> GetByIdAsync(int id);
        Task<BartenderDto> CreateAsync(BartenderDto dto);
        Task<bool> UpdateAsync(int id, BartenderDto dto);
        Task<bool> DeleteAsync(int id);
        Task<BartenderViewModel?> GetDetailsWithCocktailsAsync(int id);
    }
}
