using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel representing a bartender and their associated cocktails.
    /// Used to transfer data between the model and the view in the MVC pattern.
    /// </summary>
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
        /// List of cocktails created or served by the bartender.
        /// </summary>
        public List<Cocktail> Cocktails { get; set; } = new();
    }
}
