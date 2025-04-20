using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicMixology.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }

        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
    public class AlbumDTO
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }

        public List<SongDTO> Songs { get; set; } = new();
    }
}
