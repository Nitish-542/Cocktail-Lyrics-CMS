using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a category of cocktails in the database.
    /// </summary>
    public class CocktailCategory
    {
        /// <summary>
        /// Internal field for CategoryID (not used externally).
        /// </summary>
        internal readonly int CategoryID;

        /// <summary>
        /// Primary key for the CocktailCategory.
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the cocktail category (e.g., Classic, Tropical, Non-Alcoholic).
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Navigation property representing the list of cocktails under this category.
        /// </summary>
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for CocktailCategory, used to transfer category data including cocktails.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// List of cocktails associated with this category, represented as DTOs.
        /// </summary>
        public List<CocktailDTO> Cocktails { get; set; }
    }
}
