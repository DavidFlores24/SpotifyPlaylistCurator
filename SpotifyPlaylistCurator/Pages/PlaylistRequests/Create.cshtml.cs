using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpotifyPlaylistCurator.Pages.PlaylistRequests
{
    public class CreateModel : PageModel
    {
        private readonly SpotifyPlaylistCurator.Models.AppDbContext _context;
        public List<SelectListItem> Genres;

        public CreateModel(Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Genres = _context.Genres.Select(x => 
                                            new SelectListItem
                                            {
                                                Value = x.Id,
                                                Text = x.Name
                                            }).ToList();
            return Page();
        }

        [BindProperty]
        public Models.PlaylistRequest PlaylistRequest { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.PlaylistRequests.Add(PlaylistRequest);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
