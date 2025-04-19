using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    // ViewModel representing a cocktail category with related data.
    public class CategoryViewModel
    {
        // Unique identifier for the category.
        public int CategoryId { get; set; }

        // Name of the category, with validation for required input.
        [Required(ErrorMessage = "Category name is required.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        // List of all categories, typically used on the Index page for listing.
        public List<CocktailCategory> Categories { get; set; }

        // List of cocktails associated with this category, typically used on the Details page.
        public List<Cocktail> Cocktails { get; set; }
    }
}
