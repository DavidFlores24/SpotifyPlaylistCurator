using System;
using System.Net.Http;
using Newtonsoft.Json;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator
{
    public static class ClientApi
    {
        public static SpotifyUser QueryServiceForCurrentUser(AuthenticationObject authObj)
        {
            SpotifyUser user = new SpotifyUser();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var response = client.GetAsync($"https://api.spotify.com/v1/me").Result;

                var responseContent = response.Content;

                var responseString = response.Content.ReadAsStringAsync().Result;

                user = JsonConvert.DeserializeObject<SpotifyUser>(responseString);
            }

            return user;
        }
    }
}
