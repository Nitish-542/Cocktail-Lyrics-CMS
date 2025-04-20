using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    public class CocktailCategory
    {
        internal readonly int CategoryID;

        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Cocktail> Cocktails { get; set; }
    }
    public class CategoryDTO
    {
        public int CategoryId { get; set; }  
        public string CategoryName { get; set; }
        public List<CocktailDTO> Cocktails { get; set; }
    }
}
