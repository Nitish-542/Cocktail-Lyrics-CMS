using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel used for creating or editing a Song entity with support for dropdown selection.
    /// </summary>
    public class SongViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the song.
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Gets or sets the title of the song.
        /// This field is required.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the selected artist's ID.
        /// This field is required and displayed as "Artist" in the UI.
        /// </summary>
        [Display(Name = "Artist")]
        [Required]
        public int? ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the selected album's ID.
        /// Displayed as "Album" in the UI.
        /// </summary>
        [Display(Name = "Album")]
        public int? AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the genre of the song.
        /// This field is required.
        /// </summary>
        [Required]
        public string Genre { get; set; }

        /// <summary>
        /// List of artists for the dropdown in the view.
        /// Populated by the controller to support artist selection.
        /// </summary>
        public List<SelectListItem> ArtistList { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// List of albums for the dropdown in the view.
        /// Populated by the controller to support album selection.
        /// </summary>
        public List<SelectListItem> AlbumList { get; set; } = new List<SelectListItem>();
    }
}
