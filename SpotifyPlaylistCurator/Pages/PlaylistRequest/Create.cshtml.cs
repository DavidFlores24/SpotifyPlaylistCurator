using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator.Pages.PlaylistRequest
{
    public class CreateModel : PageModel
    {
        private readonly Models.AppDbContext _context;

        public CreateModel(Models.AppDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            PlaylistsToLook = PlaylistRequestService.GetUserPlaylists(_context);
        }

        public void OnPost(string[] playlistsToLook)
        {
            if(PlaylistsToLook == null || PlaylistsToLook.Count == 0)
                PlaylistsToLook = PlaylistRequestService.GetUserPlaylists(_context);

            var playlistsToAdd = new List<SpotifyPlaylist>();
            foreach (var playlistId in playlistsToLook)
            {
                playlistsToAdd.Add(PlaylistsToLook.First(x => x.id.Equals(playlistId)));
            }

            PlaylistRequest.PlaylistsToLook = playlistsToAdd;
            _context.PlaylistRequests.Add(PlaylistRequest);

            PlaylistRequestService.ProcessPlaylistRequest(PlaylistRequest, _context);
        }

        [BindProperty]
        public List<SpotifyPlaylist> PlaylistsToLook { get ; set; }
        
        [BindProperty]
        public Models.PlaylistRequest PlaylistRequest { get; set; }
    }
}
