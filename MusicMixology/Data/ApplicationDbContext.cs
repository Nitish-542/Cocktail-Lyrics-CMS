using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Models;

namespace MusicMixology.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailCategory> Categories { get; set; }
        public DbSet<Bartender> Bartenders { get; set; }

        // Music Models
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

        // Bridge Table
        public DbSet<CocktailSongPairing> CocktailSongPairings { get; set; }

    }
}
