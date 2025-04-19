using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents an album in the system.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// Gets or sets the unique identifier for the album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public string AlbumTitle { get; set; }

        /// <summary>
        /// Gets or sets the artist ID associated with the album.
        /// </summary>
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Navigation property for the artist associated with the album.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Navigation property for the songs in the album.
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }
    }

    /// <summary>
    /// Data transfer object (DTO) for an album.
    /// Used for transferring album data without exposing domain models.
    /// </summary>
    public class AlbumDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the album.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the title of the album.
        /// </summary>
        public string AlbumTitle { get; set; }

        /// <summary>
        /// Gets or sets the artist ID associated with the album.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist associated with the album.
        /// </summary>
        public string? ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the list of songs in the album.
        /// </summary>
        public List<SongDTO> Songs { get; set; } = new();
    }
}
