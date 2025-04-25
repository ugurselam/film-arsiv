using System.Diagnostics;
using Film_Arsiv.Data;
using Film_Arsiv.Models;
using Film_Arsiv.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Film_Arsiv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilmService _filmService;
        private readonly ITranslateService _translateService;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IFilmService filmService, ITranslateService translateService, ApplicationDbContext context)
        {
            _logger = logger;
            _filmService = filmService;
            _context = context;
            _translateService = translateService;
        }

        public IActionResult Index(int page = 1)
        {
            ViewData["Action"] = "Index";

            var films = _context.Films.Skip((page - 1) * 15).Take(15).ToList();
            double totalPage = Math.Ceiling((double)_context.Films.Count() / 15);
            bool hasNextPage = page != totalPage;

            return View((films, page, hasNextPage));
        }

        public IActionResult Film()
        {
            ViewData["Action"] = "Film";
            return View();
        }

        public async Task<IActionResult> SearchFilm(string query = "", int searchSource = 1)
        {
            ViewData["Action"] = "Film";
            var films = _context.Films.AsEnumerable()
                      .Where(x => x.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                      .ToList();

            if (searchSource == 0 && !string.IsNullOrWhiteSpace(query)) // API'de arama
            {
                var apiFilms = await _filmService.SearchFilmsAsync(query);
                films = apiFilms.Where(film => !films.Select(x => x.Title).Contains(film.Title)).ToList();

                return Json(films);
            }
            else // Veritabanýnda arama
            {
                if (films.Count == 0)
                    films = _context.Films.ToList();

                return Json(films);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddToArchive(string imdbId)
        {
            var film = await _filmService.GetFilmDetailsAsync(imdbId);
            if (film != null)
            {
                film.Plot = await _translateService.TranslateTextAsync(film.Plot);
                film.Genre = await _translateService.TranslateTextAsync(film.Genre);
                film.Runtime = await _translateService.TranslateTextAsync(film.Runtime);
                _context.Films.Add(film);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult FilmDetail(int id, int page = 1)
        {
            double totalPage = Math.Ceiling((double)_context.Comments.Where(x => x.FilmID == id).Count() / 10);
            bool hasNextPage = page != totalPage;

            var film = _context.Films.Include(c => c.Comments.Skip((page - 1) * 10).Take(10)).Where(x => x.ID == id).FirstOrDefault();
            return View((film, hasNextPage, page));
        }

        [HttpPost]
        public IActionResult AddToComment(Comments comment)
        {
            comment.CreatedAt = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("FilmDetail", new { id = comment.FilmID });
        }

    }
}
