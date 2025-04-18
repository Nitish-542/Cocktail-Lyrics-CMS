using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        // List of all categories (for Index page)
        public List<CocktailCategory> Categories { get; set; }

        // List of cocktails associated with this category (for Details page)
        public List<Cocktail> Cocktails { get; set; }

    }
}
