using System;
using Microsoft.EntityFrameworkCore;

namespace SpotifyPlaylistCurator.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistRequest> PlaylistRequests { get; set; }
        public DbSet<AuthenticationObject> AuthenticationObjects { get; set; }
    }
}
