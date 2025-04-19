using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    // Represents a Bartender entity in the system.
    public class Bartender
    {
        // Primary key for Bartender.
        [Key]
        public int BartenderId { get; set; }

        // Name of the bartender.
        public string Name { get; set; }

        // Navigation property for related Cocktails.
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }

    // Data transfer object (DTO) for Bartender, used for transferring bartender data.
    public class BartenderDto
    {
        // Unique identifier for Bartender.
        public int BartenderId { get; set; }

        // Name of the Bartender.
        public string Name { get; set; }
    }
}
