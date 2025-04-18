using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    public class PairingViewModel
    {
        public int PairingId { get; set; }

        [Required(ErrorMessage = "Please select a cocktail")]
        [Display(Name = "Cocktail")]
        public int CocktailId { get; set; }

        public string? CocktailName { get; set; }

        [Required(ErrorMessage = "Please select a song")]
        [Display(Name = "Song")]
        public int SongId { get; set; }

        public string? SongTitle { get; set; }

        [Required(ErrorMessage = "Mood category is required")]
        [Display(Name = "Mood Category")]
        public string MoodCategory { get; set; }

        // Dropdowns
        public List<SelectListItem> CocktailList { get; set; } = new();
        public List<SelectListItem> SongList { get; set; } = new();
    }
}
