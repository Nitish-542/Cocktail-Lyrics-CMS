using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel used to represent an Artist along with their associated albums and songs.
    /// This is used for views like Create, Edit, or Details.
    /// </summary>
    public class ArtistViewModel
    {
        /// <summary>
        /// Unique identifier for the artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Name of the artist. This field is required and has a maximum length of 100 characters.
        /// </summary>
        [Required(ErrorMessage = "Artist name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// List of albums associated with the artist (optional).
        /// Used primarily for displaying in the Details view.
        /// </summary>
        public List<AlbumDTO>? Albums { get; set; }

        /// <summary>
        /// List of songs associated with the artist (optional).
        /// Used primarily for displaying in the Details view.
        /// </summary>
        public List<SongDTO>? Songs { get; set; }
    }
}
