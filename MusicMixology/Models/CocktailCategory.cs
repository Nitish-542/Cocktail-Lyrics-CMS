using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    // Represents a category of cocktails in the system.
    public class CocktailCategory
    {
        // Internal read-only field for CategoryID (not used in this class directly).
        internal readonly int CategoryID;

        // Primary key for the CocktailCategory.
        [Key]
        public int CategoryId { get; set; }

        // Name of the category (e.g., "Tropical", "Classic").
        public string CategoryName { get; set; }

        // Navigation property representing the collection of cocktails associated with this category.
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }

    // Data transfer object (DTO) for the CocktailCategory.
    public class CategoryDTO
    {
        // Unique identifier for the category.
        public int CategoryId { get; set; }

        // Name of the category.
        public string CategoryName { get; set; }
    }
}
