using System;
using System.IO;
using System.Net;
using System.Text;
using SpotifyPlaylistCurator.Models;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Auth;

namespace SpotifyPlaylistCurator
{
    public static class SpotifyAuthentication
    {
        private static readonly string ClientId = "c53dddffc9054b819be83e2a53a9c148";
        private static readonly string ClientSecret = "285cd151d5244adc89c0d55bc702bf67";
        private static readonly string ReturnURL = "";

        public static void AuthenticateUser()
        {
            var authObject = new ClientCredentialsAuth
            {
                ClientSecret = ClientSecret,
                ClientId = ClientId,
                Scope = Scope.PlaylistReadPrivate
            };

            authObject.DoAuth();
        }

        //public static string BuildAuthenticationURL(AuthenticationObject authObject)
        //{
        //    var encodedScopes = string.Join(" ", authObject.Scopes);
        //    return $"https://accounts.spotify.com/authorize?response_type=code&client_id={ClientId}&scope={encodedScopes}&redirect_uri={ReturnURL}";
        //}

        public static void Authenticate()
        {
            //var requestTokenUrl = $"https://accounts.spotify.com/api/token";
            //var encodedAuth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}");

            //var webRequest = (HttpWebRequest)WebRequest.Create(requestTokenUrl);

            //webRequest.Method = "POST";
            //webRequest.ContentType = "application/x-www-form-urlencoded";
            //webRequest.Accept = "application/json";
            //webRequest.Headers.Add("Authorization: Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(encodedAuth)));

            //var request = "grant_type=client_credentials";
            //var requestBytes = Encoding.ASCII.GetBytes(request);
            //webRequest.ContentLength = requestBytes.Length;

            //var stream = webRequest.GetRequestStream();
            //stream.Write(requestBytes, 0, requestBytes.Length);
            //stream.Close();

            //var response = (HttpWebResponse)webRequest.GetResponse();
            //var json = "";

            //using (var responseStream = response.GetResponseStream())
            //{
            //    using (var reader = new StreamReader(responseStream, Encoding.UTF8))
            //    {

            //    }
            //}
        }
    }
}
