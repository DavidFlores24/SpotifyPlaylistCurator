using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator
{
    public static class PlaylistRequestService
    {
        private static readonly string ClientId = "c53dddffc9054b819be83e2a53a9c148";

        public static async Task GetUserPlaylists(AppDbContext context)
        {
            var authObj = context.AuthenticationObjects.FirstOrDefault();

            if (authObj == null)
                return;

            SpotifyUser user = new SpotifyUser();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var response = client.GetAsync($"https://api.spotify.com/v1/me").Result;

                var responseContent = response.Content;

                var responseString = response.Content.ReadAsStringAsync().Result;

                user = JsonConvert.DeserializeObject<SpotifyUser>(responseString);
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var response = client.GetAsync($"https://api.spotify.com/v1/users/{user.id}/playlists").Result;

                // TODO populate a SpotifyPlaylist object with the response and then populate form with this
            }

            return;
        }
    }

    internal class SpotifyUser
    {
        public string id { get; set; }
    }
}
