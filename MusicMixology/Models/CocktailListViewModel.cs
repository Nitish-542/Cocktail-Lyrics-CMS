using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    public class CocktailListViewModel
    {
        public IEnumerable<CocktailDTO> Cocktails { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Bartenders { get; set; }

        public int? SelectedCategoryId { get; set; }
        public int? SelectedBartenderId { get; set; }
    }
}
