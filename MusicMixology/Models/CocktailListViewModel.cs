using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    // ViewModel to represent a list of cocktails with filters for categories and bartenders.
    public class CocktailListViewModel
    {
        /// <summary>
        /// Collection of cocktail data transfer objects (DTOs) to be displayed.
        /// </summary>
        public IEnumerable<CocktailDTO> Cocktails { get; set; }

        /// <summary>
        /// List of categories as select list items for filtering cocktails by category.
        /// </summary>
        public IEnumerable<SelectListItem> Categories { get; set; }

        /// <summary>
        /// List of bartenders as select list items for filtering cocktails by bartender.
        /// </summary>
        public IEnumerable<SelectListItem> Bartenders { get; set; }

        /// <summary>
        /// Selected category ID for filtering cocktails.
        /// Nullable to allow no selection (i.e., show all categories).
        /// </summary>
        public int? SelectedCategoryId { get; set; }

        /// <summary>
        /// Selected bartender ID for filtering cocktails.
        /// Nullable to allow no selection (i.e., show all bartenders).
        /// </summary>
        public int? SelectedBartenderId { get; set; }
    }
}
