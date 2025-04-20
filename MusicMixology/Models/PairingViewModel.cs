using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel representing the pairing of a cocktail and a song, along with mood category and dropdown lists for form population.
    /// </summary>
    public class PairingViewModel
    {
        /// <summary>
        /// Unique identifier for the pairing.
        /// </summary>
        public int PairingId { get; set; }

        /// <summary>
        /// ID of the selected cocktail.
        /// Required field with validation message if not selected.
        /// </summary>
        [Required(ErrorMessage = "Please select a cocktail")]
        [Display(Name = "Cocktail")]
        public int CocktailId { get; set; }

        /// <summary>
        /// Display name of the selected cocktail (for reference/display purposes).
        /// </summary>
        public string? CocktailName { get; set; }

        /// <summary>
        /// ID of the selected song.
        /// Required field with validation message if not selected.
        /// </summary>
        [Required(ErrorMessage = "Please select a song")]
        [Display(Name = "Song")]
        public int SongId { get; set; }

        /// <summary>
        /// Display title of the selected song (for reference/display purposes).
        /// </summary>
        public string? SongTitle { get; set; }

        /// <summary>
        /// Mood category representing the vibe or theme of the pairing.
        /// Required field with validation message.
        /// </summary>
        [Required(ErrorMessage = "Mood category is required")]
        [Display(Name = "Mood Category")]
        public string MoodCategory { get; set; }

        /// <summary>
        /// List of cocktails for populating the dropdown menu in the view.
        /// </summary>
        public List<SelectListItem> CocktailList { get; set; } = new();

        /// <summary>
        /// List of songs for populating the dropdown menu in the view.
        /// </summary>
        public List<SelectListItem> SongList { get; set; } = new();
    }
}
