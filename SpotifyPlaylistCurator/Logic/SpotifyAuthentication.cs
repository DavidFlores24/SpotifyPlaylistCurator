using System;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator
{
    public static class SpotifyAuthentication
    {
        private static readonly string ClientId = "c53dddffc9054b819be83e2a53a9c148";
        private static readonly string ClientSecret = "285cd151d5244adc89c0d55bc702bf67";
        private static readonly string ReturnURL = "";

        public static void AuthenticateUser()
        {

        }

        public static string BuildAuthenticationURL(AuthenticationObject authObject)
        {
            var encodedScopes = String.Join("%20", authObject.Scopes);
            return $"https://accounts.spotify.com/authorize?response_type=code&client_id={ClientId}&scope={encodedScopes}&redirect_uri={ReturnURL}";
        }

        public static void SaveAuthenticationCode(AuthenticationObject authObject, string code)
        {

        }
    }
}
