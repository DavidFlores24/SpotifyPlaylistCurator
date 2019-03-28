using System;
using Microsoft.EntityFrameworkCore;

namespace SpotifyPlaylistCurator.Models
{
    public class AuthenticationObjectContext : DbContext
    {
        public AuthenticationObjectContext(DbContextOptions<AuthenticationObjectContext> options)
            : base(options)
        {
        }

        public DbSet<SpotifyPlaylistCurator.Models.AuthenticationObject> AuthenticationObject { get; set; }
    }
}
