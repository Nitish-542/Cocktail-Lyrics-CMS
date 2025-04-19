using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents an artist with properties for artist ID, name, and associated songs and albums.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Gets or sets the unique identifier for the artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of songs associated with the artist.
        /// </summary>
        public virtual ICollection<Song> Songs { get; set; }

        /// <summary>
        /// Gets or sets the collection of albums associated with the artist.
        /// </summary>
        public virtual ICollection<Album> Albums { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for transferring artist information.
    /// </summary>
    public class ArtistDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the artist.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the name of the artist.
        /// </summary>
        public string Name { get; set; }
    }
}
