using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyPlaylistCurator.Models
{
    public class PlaylistRequest
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }

        [NotMapped]
        public IEnumerable<Genre> Genres { get; set; }

        public PlaylistRequest()
        {
        }
    }
}
