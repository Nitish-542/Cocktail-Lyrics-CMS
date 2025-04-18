using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMixology.Models
{
    public class Song
    {
        public int SongId { get; set; }

        public string Title { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        [ForeignKey("Album")]
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public string Genre { get; set; }

        public virtual ICollection<CocktailSongPairing> Pairings { get; set; }
    }

    public class SongDTO
    {
        public int SongId { get; set; }

        public  string Title { get; set; }

        public int ArtistId { get; set; }

        public int AlbumId { get; set; }

        public string Genre { get; set; }

        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }

    }
}
