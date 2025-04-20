using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a Song entity in the MusicMixology system.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// Unique identifier for the song.
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Foreign key referencing the Artist of the song.
        /// </summary>
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }

        /// <summary>
        /// Navigation property for the associated Artist.
        /// </summary>
        public virtual Artist Artist { get; set; }

        /// <summary>
        /// Foreign key referencing the Album the song belongs to.
        /// </summary>
        [ForeignKey("Album")]
        public int AlbumId { get; set; }

        /// <summary>
        /// Navigation property for the associated Album.
        /// </summary>
        public virtual Album Album { get; set; }

        /// <summary>
        /// Genre of the song (e.g., Rock, Jazz, Pop).
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Collection of cocktail-song pairings associated with the song.
        /// </summary>
        public virtual ICollection<CocktailSongPairing> Pairings { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for the Song entity, used to simplify API responses.
    /// </summary>
    public class SongDTO
    {
        /// <summary>
        /// Unique identifier for the song.
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Identifier for the artist of the song.
        /// </summary>
        public int ArtistId { get; set; }

        /// <summary>
        /// Identifier for the album the song belongs to.
        /// </summary>
        public int AlbumId { get; set; }

        /// <summary>
        /// Genre of the song.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Name of the artist (for display purposes).
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Title of the album (for display purposes).
        /// </summary>
        public string AlbumTitle { get; set; }
    }
}
