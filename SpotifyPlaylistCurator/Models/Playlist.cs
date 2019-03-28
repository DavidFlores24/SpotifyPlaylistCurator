using System;
using System.Collections;

namespace SpotifyPlaylistCurator.Models
{
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable Songs { get; set; }
        public IEnumerable Genres { get; set; }

        public Playlist()
        {

        }
    }
}
