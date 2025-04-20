using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    public class BartenderViewModel
    {
        public int BartenderId { get; set; }
        public string Name { get; set; }
        public List<Cocktail> Cocktails { get; set; } = new();
    }
}
