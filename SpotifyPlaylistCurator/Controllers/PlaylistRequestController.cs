using System;
using Microsoft.AspNetCore.Mvc;
using SpotifyPlaylistCurator.Models;

namespace SpotifyPlaylistCurator.Controllers
{
    public class PlaylistRequestController : Controller
    {
        private readonly AppDbContext _context;
        public PlaylistRequestController()
        {
        }

        public IActionResult Index()
        {
            ViewBag.ListofGenres = _context.Genres;
            return View();
        }
    }
}
