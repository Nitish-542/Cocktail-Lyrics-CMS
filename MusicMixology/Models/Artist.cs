using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
    public class ArtistDTO
    {
        public int ArtistId { get; set; }
        public  string Name { get; set; }

    }
}
