using Microsoft.AspNetCore.Mvc.Rendering;
using MusicMixology.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel representing album data for Create, Edit, and Display views.
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Unique identifier for the album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Title of the album. This field is required.
        /// </summary>
        [Required(ErrorMessage = "Album title is required")]
        [Display(Name = "Album Title")]
        public string AlbumTitle { get; set; }

        /// <summary>
        /// Selected artist's ID. This field is required.
        /// </summary>
        [Required(ErrorMessage = "Artist is required")]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Name of the artist. Used for display in views.
        /// </summary>
        public string? ArtistName { get; set; }

        /// <summary>
        /// List of available artists used to populate a dropdown in the UI.
        /// </summary>
        public List<SelectListItem> ArtistList { get; set; } = new();

        /// <summary>
        /// List of songs in the album. Optional and typically used in the Details view.
        /// </summary>
        public List<SongDTO>? Songs { get; set; }
    }
}
