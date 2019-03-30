using System;
namespace SpotifyPlaylistCurator
{
    public class SpotifyUser
    {
        public string id { get; set; }
    }

    public class SpotifyPlaylist
    {
        public string id { get; set; }
        public string name { get; set; }
        public SpotifyTrackHref tracks { get; set; }
    }

    public class SpotifyPlaylistTrack
    {
        public bool is_local { get; set; }
        public SpotifyTrack track { get; set; }
    }

    public class SpotifyTrackHref
    {
        public string href { get; set; }
    }

    public class SpotifyTrack
    {
        public string id { get; set; }
        public string name { get; set; }
        public int duration_ms {get;set;}
        public bool is_playable { get; set; }
        public int popularity { get; set; }
        public string uri { get; set; }
    }
}
