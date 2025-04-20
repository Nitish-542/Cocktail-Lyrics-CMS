using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a cocktail entity with associated details and relationships.
    /// </summary>
    public class Cocktail
    {
        /// <summary>
        /// Primary key for the cocktail.
        /// </summary>
        [Key]
        public int CocktailID { get; set; }

        /// <summary>
        /// Name of the cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Recipe or preparation steps of the cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        /// <summary>
        /// Liquor ingredients used in the cocktail (optional).
        /// </summary>
        public string LiqIns { get; set; }

        /// <summary>
        /// Mixer ingredients used in the cocktail (optional).
        /// </summary>
        public string MixIns { get; set; }

        /// <summary>
        /// Foreign key to the cocktail category. Required field.
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        /// <summary>
        /// Foreign key to the bartender who created the cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Bartender is required")]
        public int BartenderID { get; set; }

        /// <summary>
        /// Navigation property to the related category entity.
        /// </summary>
        [ForeignKey("CategoryID")]
        public virtual CocktailCategory Category { get; set; }

        /// <summary>
        /// Navigation property to the related bartender entity.
        /// </summary>
        [ForeignKey("BartenderID")]
        public virtual Bartender Bartender { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for transferring cocktail data between layers.
    /// Includes category and bartender info for display purposes.
    /// </summary>
    public class CocktailDTO
    {
        /// <summary>
        /// Cocktail ID.
        /// </summary>
        public int CocktailID { get; set; }

        /// <summary>
        /// Name of the cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        /// <summary>
        /// Recipe for preparing the cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        /// <summary>
        /// Liquor ingredients used in the cocktail (optional).
        /// </summary>
        public string LiqIns { get; set; }

        /// <summary>
        /// Mixer ingredients used in the cocktail (optional).
        /// </summary>
        public string MixIns { get; set; }

        /// <summary>
        /// Category ID linked to this cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Bartender ID linked to this cocktail. Required field.
        /// </summary>
        [Required(ErrorMessage = "Bartender is required")]
        [Display(Name = "Bartender")]
        public int BartenderId { get; set; }

        /// <summary>
        /// Name of the category (for display purposes, optional).
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// Category DTO object (optional).
        /// </summary>
        public CategoryDTO? Category { get; set; }

        /// <summary>
        /// Bartender DTO object (optional).
        /// </summary>
        public BartenderDto? Bartender { get; set; }
    }
}
