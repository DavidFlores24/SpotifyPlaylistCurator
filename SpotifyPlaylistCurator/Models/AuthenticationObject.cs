using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyPlaylistCurator.Models
{
    public class AuthenticationObject
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        [NotMapped]
        public ICollection<string> Scopes { get; set; }

        public AuthenticationObject()
        {

        }
    }
}
