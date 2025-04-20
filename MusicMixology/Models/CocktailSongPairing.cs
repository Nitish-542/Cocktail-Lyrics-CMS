using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    /// <summary>
    /// Represents a pairing between a cocktail and a song based on a mood category.
    /// </summary>
    public class CocktailSongPairing
    {
        /// <summary>
        /// Unique identifier for the cocktail-song pairing.
        /// </summary>
        [Key]
        public int PairingId { get; set; }

        /// <summary>
        /// Foreign key referencing the associated cocktail.
        /// </summary>
        [ForeignKey("Cocktail")]
        public int CocktailId { get; set; }

        /// <summary>
        /// Foreign key referencing the associated song.
        /// </summary>
        [ForeignKey("Song")]
        public int SongId { get; set; }

        /// <summary>
        /// Category representing the mood of the pairing (e.g., Relaxing, Energetic).
        /// </summary>
        public string MoodCategory { get; set; }

        /// <summary>
        /// Navigation property to the related Cocktail entity.
        /// </summary>
        public virtual Cocktail Cocktail { get; set; }

        /// <summary>
        /// Navigation property to the related Song entity.
        /// </summary>
        public virtual Song Song { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) version of the CocktailSongPairing class, used for API communication.
    /// </summary>
    public class CocktailSongPairingDTO
    {
        /// <summary>
        /// Unique identifier for the cocktail-song pairing.
        /// </summary>
        public int PairingId { get; set; }

        /// <summary>
        /// ID of the paired cocktail.
        /// </summary>
        public int CocktailId { get; set; }

        /// <summary>
        /// Name of the cocktail.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DTO object representing cocktail details.
        /// </summary>
        public CocktailDTO Cocktail { get; set; }

        /// <summary>
        /// ID of the paired song.
        /// </summary>
        public int SongId { get; set; }

        /// <summary>
        /// Title of the song.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// DTO object representing song details.
        /// </summary>
        public SongDTO Song { get; set; }

        /// <summary>
        /// Category representing the mood of the pairing.
        /// </summary>
        public string MoodCategory { get; set; }
    }
}
