using System;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.Design.OperationExecutor;

namespace SpotifyPlaylistCurator.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<PlaylistRequest> PlaylistRequests { get; set; }
        public DbSet<AuthenticationObject> AuthenticationObjects { get; set; }
    }
}
