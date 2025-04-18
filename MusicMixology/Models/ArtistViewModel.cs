using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MusicMixology.Models;

namespace MusicMixology.ViewModels
{
    public class ArtistViewModel
    {
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Artist name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        // Optional: Include these for the Details view
        public List<AlbumDTO>? Albums { get; set; }
        public List<SongDTO>? Songs { get; set; }
    }
}
