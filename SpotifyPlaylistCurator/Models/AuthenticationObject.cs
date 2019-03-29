using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotifyPlaylistCurator.Models
{
    public class AuthenticationObject
    {
        public string Id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        [NotMapped]
        public ICollection<string> Scopes { get; set; }

        public AuthenticationObject()
        {

        }
    }
}
