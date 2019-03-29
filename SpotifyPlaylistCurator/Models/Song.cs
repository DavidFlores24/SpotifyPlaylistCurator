using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace SpotifyPlaylistCurator.Models
{
    public class Song
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<SongGenre> SongGenres { get; set; }

        public Song()
        {
        }
    }
}
