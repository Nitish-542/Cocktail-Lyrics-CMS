using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel representing a cocktail category and its related data.
    /// Used for both displaying a list of categories and showing details for a specific category.
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// The unique identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The name of the category.
        /// This field is required and displayed as "Category Name" in the UI.
        /// </summary>
        [Required(ErrorMessage = "Category name is required.")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// A list of all cocktail categories.
        /// Used for the Index page to display multiple categories.
        /// </summary>
        public List<CocktailCategory> Categories { get; set; }

        /// <summary>
        /// A list of cocktails that belong to this category.
        /// Used on the Details page to show related cocktails.
        /// </summary>
        public List<Cocktail> Cocktails { get; set; }
    }
}
