using System;
using System.Collections.Generic;

namespace SpotifyPlaylistCurator.Models
{
    public class PlaylistRequest
    {
        public int Id { get; set; }
        public int Duration  { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<string> PlaylistsToLook { get; set; }

        public PlaylistRequest()
        {
        }
    }
}
