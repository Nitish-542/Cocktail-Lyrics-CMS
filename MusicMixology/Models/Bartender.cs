using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    public class Bartender
    {

        [Key]
        public int BartenderId { get; set; }

        public string Name { get; set; }
        // Navigation
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }
    public class BartenderDto
    {
        public int BartenderId { get; set; }
        public string Name { get; set; }
    }
}
