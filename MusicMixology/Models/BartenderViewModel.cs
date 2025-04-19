using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    // ViewModel representing a bartender and the cocktails they serve.
    public class BartenderViewModel
    {
        /// <summary>
        /// Unique identifier for the bartender.
        /// </summary>
        public int BartenderId { get; set; }

        /// <summary>
        /// Name of the bartender.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of cocktails associated with the bartender.
        /// </summary>
        public List<Cocktail> Cocktails { get; set; } = new();
    }
}
