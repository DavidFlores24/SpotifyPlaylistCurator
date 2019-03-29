using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyPlaylistCurator.Models
{
    public class PlaylistSong
    {
        public int Id { get; set; }

        public Playlist Playlist { get; set; }
        public Song Song { get; set; }
    }
}
