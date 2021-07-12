using FilmsCatalog.Data;
using FilmsCatalog.Models;
using FilmsCatalog.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FilmsDbContext db;
        public int pageSize = 5;


        public HomeController(ILogger<HomeController> logger, FilmsDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(int page = 1)
        {
            IEnumerable<Film> source = db.Films;

            int count = source.Count();

            var items = source
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            FilmsViewModel model = new FilmsViewModel
            {
                Films = items,
                PageViewModel = pageViewModel
            };

            return View(model);
        }

        // ==== Просмор/ Добавление/ Редактирование/
        public IActionResult DetailFilm(int id, Mode mode, int page = 1)
        {
            Film item = (id == 0) ? new Film() : db.Films.Find(id);

            if (item.UserName != HttpContext.User.Identity.Name && mode != Mode.Add)
            {
                mode = Mode.View;
            }
            else if (mode == Mode.Add)
            {
                item.UserName = HttpContext.User.Identity.Name;
            }

            FilmViewModel model = new FilmViewModel
            {
                Film = item,
                Mode = mode,
                Page = page
            };

            return View(model);
        }

        // ====== Сохранение
        [HttpPost]
        public IActionResult DetailFilm(FilmViewModel filmviewmodel)
        {
            Film film = filmviewmodel.Film;
            IFormFile posterfile = filmviewmodel.PosterFile;

            if (ModelState.IsValid)
            {
                if (posterfile != null)  // сохранение изображения
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(posterfile.OpenReadStream())) 
                    {
                        imageData = binaryReader.ReadBytes((int)posterfile.Length);
                    }
                    film.Poster = imageData;
                }

                if (film.Id == 0)
                {
                    film.UserName = HttpContext.User.Identity.Name;
                    db.Films.Add(film);
                }
                else
                {
                    Film filmdb = db.Films.Find(film.Id);
                    filmdb.Name = film.Name;
                    filmdb.Year = film.Year;
                    filmdb.Desc = film.Desc;
                    filmdb.Producer = film.Producer;
                    filmdb.Poster = film.Poster ?? filmdb.Poster;
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { page = filmviewmodel.Page });
            }
            else
            {
                FilmViewModel model = new FilmViewModel
                {
                    Film = film,
                    Mode = filmviewmodel.Mode,
                    Page = filmviewmodel.Page
                };

                return View(model);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
