using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album title is required")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        // Display name for views
        public string? ArtistName { get; set; }

        // Dropdown list of available artists for Create/Edit
        public List<SelectListItem> ArtistList { get; set; } = new();

        // Optional: List of songs (for details view)
        public List<SongDTO>? Songs { get; set; }
    }
}
