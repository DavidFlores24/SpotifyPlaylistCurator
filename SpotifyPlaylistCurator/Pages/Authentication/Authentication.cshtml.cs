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

            AuthenticationObject = _context.AuthenticationObjects.FirstOrDefault();
        }

        public async Task OnGetAsync()
        {
            if (AuthenticationObject == null)
            {
                AuthenticationObject = new Models.AuthenticationObject();
                _context.Add(AuthenticationObject);
            }

            if(!string.IsNullOrEmpty(code))
            {
                AuthenticationObject = SpotifyAuthentication.ExchangeCodeForToken(AuthenticationObject, code);
                _context.Update(AuthenticationObject);
                await _context.SaveChangesAsync();
            }
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