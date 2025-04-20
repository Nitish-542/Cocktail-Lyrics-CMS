using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MusicMixology.Models
{
    public class CocktailSongPairing
    {
        [Key]
        public int PairingId { get; set; }

        [ForeignKey("Cocktail")]
        public int CocktailId { get; set; }

        [ForeignKey("Song")]
        public int SongId { get; set; }

        public string MoodCategory { get; set; }

        public virtual Cocktail Cocktail { get; set; }
        public virtual Song Song { get; set; }
    }
    public class CocktailSongPairingDTO
    {
        public int PairingId { get; set; }

        public int CocktailId { get; set; }
        public string Name { get; set; }
        public CocktailDTO Cocktail { get; set; }

        public int SongId { get; set; }
        public string Title { get; set; }
        public SongDTO Song{ get; set; }

        public string MoodCategory { get; set; }
    }

}
