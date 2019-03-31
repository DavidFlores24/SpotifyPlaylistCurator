using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator
{
    public static class PlaylistRequestService
    {
        public static List<SpotifyPlaylist> GetUserPlaylists(AppDbContext context)
        {
            var authObj = context.AuthenticationObjects.FirstOrDefault();
            var result = new List<SpotifyPlaylist>();

            if (authObj == null)
                return result;

            var user = ClientApi.QueryServiceForCurrentUser(authObj);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var response = client.GetAsync($"https://api.spotify.com/v1/users/{user.id}/playlists").Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var jobj = JObject.Parse(responseContent);
                var playlists = JsonConvert.DeserializeObject<List<SpotifyPlaylist>>(jobj["items"].ToString());

                result = playlists.ToList();
            }

            return result;
        }

        public static void ProcessPlaylistRequest(PlaylistRequest request, AppDbContext context)
        {
            var authObj = context.AuthenticationObjects.FirstOrDefault();
            if (authObj == null)
                return;

            var user = ClientApi.QueryServiceForCurrentUser(authObj);

            var playlist = CreatePlaylist(request, authObj, user);
            var tracks = GenerateTracksForPlaylist(authObj, request).ToList();

            AddTracksToPlaylist(authObj, playlist, tracks);
        }

        private static SpotifyPlaylist CreatePlaylist(PlaylistRequest request, AuthenticationObject authObj, SpotifyUser user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"https://api.spotify.com/v1/users/{user.id}/playlists")
                {
                    Content = new StringContent($"{{\"name\":\"{request.Name}\"}}",
                    Encoding.UTF8,
                    "application/json")
                };

                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.SendAsync(httpRequest).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<SpotifyPlaylist>(responseContent);
            }
        }

        private static void AddTracksToPlaylist(AuthenticationObject authObj, SpotifyPlaylist playlist, List<SpotifyTrack> tracks)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var trackUris = "";
                foreach(var uri in tracks.Select(x => x.uri))
                {
                    trackUris += $"\"{uri}\",";
                }

                trackUris = trackUris.TrimEnd(',');

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"https://api.spotify.com/v1/playlists/{playlist.id}/tracks")
                {
                    Content = new StringContent($"{{\"uris\":[{trackUris}]}}",
                    Encoding.UTF8,
                    "application/json")
                };

                httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.SendAsync(httpRequest).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;
            }
        }

        private static IEnumerable<SpotifyTrack> GenerateTracksForPlaylist(AuthenticationObject authObj, PlaylistRequest request)
        {
            var tracksForPlaylist = new List<SpotifyTrack>();
            var avoidableTracks = new List<SpotifyTrack>();
            var coveredDuration = 0;
            var overshootLimit = 30000;
            var maxNumberOfTries = 50;
            var numberOfTries = 0;

            while (coveredDuration < request.Duration * 60000 - 10000 && numberOfTries < maxNumberOfTries)
            {
                foreach (var playlistToLook in request.PlaylistsToLook)
                {
                    var tracks = GetPlaylistTracks(authObj, playlistToLook).Select(x => x.track).ToList();

                    var trackToAdd = tracks
                                .OrderByDescending(x => x.popularity)
                                .First();

                    if(trackToAdd == null)
                    {
                        numberOfTries++;
                        continue;
                    }
                    if(tracksForPlaylist.Select(x => x.id).ToList().Contains(trackToAdd.id) 
                        || avoidableTracks.Select(x => x.id).ToList().Contains(trackToAdd.id))
                    {
                        var random = new Random();
                        var index = random.Next(2, tracks.Count());
                        trackToAdd = tracks.ElementAt(index);
                    }

                    if (coveredDuration + trackToAdd.duration_ms > request.Duration * 60000 + overshootLimit)
                    {
                        avoidableTracks.Add(trackToAdd);
                        numberOfTries++;
                        continue;
                    }

                    tracksForPlaylist.Add(trackToAdd);
                    coveredDuration += trackToAdd.duration_ms;
                }
            }
            return tracksForPlaylist;
        }

        private static IEnumerable<SpotifyPlaylistTrack> GetPlaylistTracks(AuthenticationObject authObj, SpotifyPlaylist playlist)
        {
            var playlistTracks = new List<SpotifyPlaylistTrack>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                var response = client.GetAsync($"{playlist.tracks.href}").Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;

                var jobj = JObject.Parse(responseContent);
                playlistTracks = JsonConvert.DeserializeObject<List<SpotifyPlaylistTrack>>(jobj["items"].ToString());
            }
            return playlistTracks;
        }

        public static IEnumerable<SpotifyImage> GetImagesForCarousel(AppDbContext context, List<SpotifyPlaylist> playlists)
        {
            var authObj = context.AuthenticationObjects.FirstOrDefault();
            var images = new List<SpotifyImage>();
            foreach(var playlist in playlists)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authObj.access_token);

                    var response = client.GetAsync($"https://api.spotify.com/v1/playlists/{playlist.id}/images").Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    var jobj = JArray.Parse(responseContent);
                    var playlistImages = JsonConvert.DeserializeObject<List<SpotifyImage>>(responseContent);
                    images.Add(playlistImages.First());
                }
            }
            return images;
        }
    }
}
