using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{

    public class Cocktail
    {
        [Key]
        public int CocktailID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        public string LiqIns { get; set; }
        public string MixIns { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Bartender is required")]
        public int BartenderID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual CocktailCategory Category { get; set; }

        [ForeignKey("BartenderID")]
        public virtual Bartender Bartender { get; set; }
    }

    public class CocktailDTO
    {
        public int CocktailID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Recipe is required")]
        public string Recipe { get; set; }

        public string LiqIns { get; set; }
        public string MixIns { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Bartender is required")]
        [Display(Name = "Bartender")]
        public int BartenderId { get; set; }

        public string? CategoryName { get; set; }
        public CategoryDTO? Category { get; set; }
        public BartenderDto? Bartender { get; set; }

    }


}



