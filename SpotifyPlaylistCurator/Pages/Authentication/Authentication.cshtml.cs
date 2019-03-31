using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SpotifyPlaylistCurator.Pages.Authentication
{
    public class AuthenticationModel : PageModel
    {
        private readonly SpotifyPlaylistCurator.Models.AppDbContext _context;

        public AuthenticationModel(Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            AuthenticationObject = _context.AuthenticationObjects.FirstOrDefault();

            if (!string.IsNullOrEmpty(code))
            {
                if (AuthenticationObject == null)
                {
                    SpotifyAuthentication.ExchangeCodeForToken(code, _context);
                }

                return RedirectToPage("../PlaylistRequest/Create");
            }

            return Page();
        }

        public void OnPost()
        {
            var authTokenUrl = SpotifyAuthentication.GetAuthenticationTokenURL();
            Response.Redirect(authTokenUrl);
        }

        [BindProperty(SupportsGet = true)]
        public string code { get; set; }

        public Models.AuthenticationObject AuthenticationObject { get; set; }
    }
}