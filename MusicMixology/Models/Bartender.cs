using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a Bartender entity in the database.
    /// Each bartender can create multiple cocktails.
    /// </summary>
    public class Bartender
    {
        /// <summary>
        /// Primary key for the Bartender.
        /// </summary>
        [Key]
        public int BartenderId { get; set; }

        /// <summary>
        /// Name of the bartender.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Navigation property for the list of cocktails created by the bartender.
        /// </summary>
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for transferring Bartender data,
    /// typically used in API responses to avoid exposing full entity logic.
    /// </summary>
    public class BartenderDto
    {
        /// <summary>
        /// Bartender's unique identifier.
        /// </summary>
        public int BartenderId { get; set; }

        /// <summary>
        /// Bartender's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A simplified list of cocktails associated with the bartender.
        /// This is optional and may be null.
        /// </summary>
        public List<CocktailDTO>? Cocktails { get; set; }
    }
}
