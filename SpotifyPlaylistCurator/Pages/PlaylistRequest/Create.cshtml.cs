using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        }

        public void OnPost()
        {
            PlaylistRequestService.GetUserPlaylists(_context);
        }
    }
}
