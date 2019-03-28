using System;
using System.Collections.Generic;

namespace SpotifyPlaylistCurator.Models
{
    public class AuthenticationObject
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Scopes { get; set; }

        public AuthenticationObject()
        {

        }
    }
}
