using System;
using System.Collections;

namespace SpotifyPlaylistCurator.Models
{
    public class Song
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable Genres { get; set; }

        public Song()
        {
        }
    }
}
