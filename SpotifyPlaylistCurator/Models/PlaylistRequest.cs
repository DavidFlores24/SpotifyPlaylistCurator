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
        public string Name { get; set; }

        [NotMapped]
        public IEnumerable<Genre> Genres { get; set; }

        [NotMapped]
        [Display(Name = "Playlists to Look")]
        public List<SpotifyPlaylist> PlaylistsToLook { get; set; }

        public PlaylistRequest()
        {
        }
    }
}
