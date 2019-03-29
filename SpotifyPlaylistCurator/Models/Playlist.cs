using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyPlaylistCurator.Models
{
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PlaylistSong> PlaylistSongs { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public Playlist()
        {

        }
    }
}
