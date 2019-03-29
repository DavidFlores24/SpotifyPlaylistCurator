using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyPlaylistCurator.Models
{
    public class SongGenre
    {
        public int Id { get; set; }
        public Song Song { get; set; }

        public Genre Genres { get; set; }
    }
}
