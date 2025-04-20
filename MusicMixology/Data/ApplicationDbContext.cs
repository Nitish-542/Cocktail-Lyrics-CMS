using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMixology.Models;

namespace MusicMixology.Data
{
    /// <summary>
    /// ApplicationDbContext is the main database context class for the MusicMixology application.
    /// It inherits from IdentityDbContext to include ASP.NET Core Identity functionality.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Constructor that accepts DbContextOptions and passes them to the base IdentityDbContext class.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Represents the Cocktails table in the database.
        /// </summary>
        public DbSet<Cocktail> Cocktails { get; set; }

        /// <summary>
        /// Represents the Cocktail Categories table in the database.
        /// </summary>
        public DbSet<CocktailCategory> Categories { get; set; }

        /// <summary>
        /// Represents the Bartenders table in the database.
        /// </summary>
        public DbSet<Bartender> Bartenders { get; set; }

        // Music Models

        /// <summary>
        /// Represents the Songs table in the database.
        /// </summary>
        public DbSet<Song> Songs { get; set; }

        /// <summary>
        /// Represents the Artists table in the database.
        /// </summary>
        public DbSet<Artist> Artists { get; set; }

        /// <summary>
        /// Represents the Albums table in the database.
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        // Bridge Table

        /// <summary>
        /// Represents the bridge table for many-to-many relationship 
        /// between Cocktails and Songs.
        /// </summary>
        public DbSet<CocktailSongPairing> CocktailSongPairings { get; set; }
    }
}
