using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    // Represents a cocktail with properties related to its details, recipe, and associations.
    public class Cocktail
    {
        // Unique identifier for the cocktail.
        [Key]
        public int CocktailID { get; set; }

        // Name of the cocktail (required).
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        // Recipe of the cocktail (required).
        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        // Liquor ingredients (optional).
        public string LiqIns { get; set; }

        // Mixing ingredients (optional).
        public string MixIns { get; set; }

        // Category ID for the cocktail (required).
        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        // Bartender ID who created the cocktail (required).
        [Required(ErrorMessage = "Bartender is required")]
        public int BartenderID { get; set; }

        // Navigation property for the category of the cocktail.
        [ForeignKey("CategoryID")]
        public virtual CocktailCategory Category { get; set; }

        // Navigation property for the bartender who created the cocktail.
        [ForeignKey("BartenderID")]
        public virtual Bartender Bartender { get; set; }
    }

    // Data transfer object for a cocktail, used for communication between the API and client.
    public class CocktailDTO
    {
        // Unique identifier for the cocktail.
        public int CocktailID { get; set; }

        // Name of the cocktail (required).
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        // Recipe of the cocktail (required).
        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        // Liquor ingredients (optional).
        public string LiqIns { get; set; }

        // Mixing ingredients (optional).
        public string MixIns { get; set; }

        // Category ID for the cocktail (required).
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Bartender ID who created the cocktail (required).
        [Required(ErrorMessage = "Bartender is required")]
        [Display(Name = "Bartender")]
        public int BartenderId { get; set; }

        // Optional field to hold category name.
        public string? CategoryName { get; set; }

        // Navigation property for the category of the cocktail.
        public CategoryDTO? Category { get; set; }

        // Navigation property for the bartender who created the cocktail.
        public BartenderDto? Bartender { get; set; }
    }
}
