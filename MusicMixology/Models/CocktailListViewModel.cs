using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a list of cocktails along with filtering options.
    /// Used in views where users can filter cocktails by category or bartender.
    /// </summary>
    public class CocktailListViewModel
    {
        /// <summary>
        /// A collection of cocktails to display, typically filtered by selected options.
        /// </summary>
        public IEnumerable<CocktailDTO> Cocktails { get; set; }

        /// <summary>
        /// List of available categories for dropdown filtering in the UI.
        /// </summary>
        public IEnumerable<SelectListItem> Categories { get; set; }

        /// <summary>
        /// List of available bartenders for dropdown filtering in the UI.
        /// </summary>
        public IEnumerable<SelectListItem> Bartenders { get; set; }

        /// <summary>
        /// ID of the selected category used for filtering cocktails.
        /// </summary>
        public int? SelectedCategoryId { get; set; }

        /// <summary>
        /// ID of the selected bartender used for filtering cocktails.
        /// </summary>
        public int? SelectedBartenderId { get; set; }
    }
}
