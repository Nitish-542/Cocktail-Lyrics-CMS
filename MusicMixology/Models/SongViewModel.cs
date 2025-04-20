using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.ViewModels
{
    public class SongViewModel
    {
        public int SongId { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Artist")]
        [Required]
        public int? ArtistId { get; set; }

        [Display(Name = "Album")]
        public int? AlbumId { get; set; }

        [Required]
        public string Genre { get; set; }

        // ✅ These are required for dropdowns
        public List<SelectListItem> ArtistList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AlbumList { get; set; } = new List<SelectListItem>();
    }
}
