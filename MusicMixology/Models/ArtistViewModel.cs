using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    /// <summary>
    /// ViewModel representing an artist with related albums and songs.
    /// </summary>
    public class ArtistViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        /// <remarks>
        /// The name is required and must be less than or equal to 100 characters.
        /// </remarks>
        [Required(ErrorMessage = "Artist name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Optional list of albums associated with the artist.
        /// </summary>
        /// <remarks>
        /// This is used for the artist's details view.
        /// </remarks>
        public List<AlbumDTO>? Albums { get; set; }

        /// <summary>
        /// Optional list of songs associated with the artist.
        /// </summary>
        /// <remarks>
        /// This is used for the artist's details view.
        /// </remarks>
        public List<SongDTO>? Songs { get; set; }
    }
}
