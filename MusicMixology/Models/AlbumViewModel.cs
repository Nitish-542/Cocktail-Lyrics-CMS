using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    // ViewModel for managing album-related data, used for Create/Edit operations.
    public class AlbumViewModel
    {
        // Unique identifier for the album.
        public int AlbumId { get; set; }

        // Required field with a custom error message if not provided.
        [Required(ErrorMessage = "Album title is required")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        // Required field for the artist, with a custom error message.
        [Required(ErrorMessage = "Artist is required")]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        // Optional field to store the artist's name for display purposes.
        public string? ArtistName { get; set; }

        // List of available artists to populate a dropdown for Create/Edit views.
        public List<SelectListItem> ArtistList { get; set; } = new();

        // Optional list of songs associated with the album, typically used for the details view.
        public List<SongDTO>? Songs { get; set; }
    }
}
