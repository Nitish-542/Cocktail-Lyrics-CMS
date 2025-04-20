using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a music album entity in the database.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Primary key for the Album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Title of the album.
        /// </summary>
        public string AlbumTitle { get; set; }

        /// <summary>
        /// Foreign key referencing the Artist who created the album.
        /// </summary>
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Navigation property to the related Artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Collection of songs that belong to this album.
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for Album, used for API responses or view models.
    /// </summary>
    public class AlbumDTO
    {
        /// <summary>
        /// Identifier for the album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Title of the album.
        /// </summary>
        public string AlbumTitle { get; set; }

        /// <summary>
        /// Identifier for the associated artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Name of the artist (optional).
        /// </summary>
        public string? ArtistName { get; set; }

        /// <summary>
        /// List of songs in the album represented by DTOs.
        /// </summary>
        public List<SongDTO> Songs { get; set; } = new();
    }
}
