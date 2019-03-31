using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SpotifyPlaylistCurator.Models;
using Newtonsoft.Json;

namespace SpotifyPlaylistCurator
{
    public static class SpotifyAuthentication
    {
        private static readonly string ClientId = "c53dddffc9054b819be83e2a53a9c148";
        private static readonly string ClientSecret = "285cd151d5244adc89c0d55bc702bf67";
        private static readonly string ReturnURL = "https://localhost:5001/Authentication/Authentication";

        internal static void ExchangeCodeForToken(string code, AppDbContext context)
        {
            var responseString = "";
            var authenticationObject = new AuthenticationObject();
            if(code.Length > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    string bearer = Convert.ToBase64String(Encoding.ASCII.GetBytes(ClientId + ":" + ClientSecret));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", bearer);

                    FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string ,string>("code", code),
                        new KeyValuePair<string ,string>("redirect_uri", ReturnURL),
                        new KeyValuePair<string ,string>("grant_type", "authorization_code")
                    });

                    var response = client.PostAsync("https://accounts.spotify.com/api/token", formContent).Result;
                    var responseContent = response.Content;

                    responseString = response.Content.ReadAsStringAsync().Result;

                    authenticationObject = JsonConvert.DeserializeObject<AuthenticationObject>(responseString);
                    context.Add(authenticationObject);
               }

                context.SaveChanges();
            }
        }


        public static string GetAuthenticationTokenURL()
        {
            var qb = new QueryBuilder();
            qb.Add("response_type", "code");
            qb.Add("client_id", ClientId);
            qb.Add("scope", string.Join(" ", new List<string>
            {
                "playlist-modify-public",
                "user-library-read",
                "playlist-modify-public"
            }));
            qb.Add("redirect_uri", ReturnURL);

            return $"https://accounts.spotify.com/authorize/{qb.ToQueryString().ToString()}";
        }
    }
}
