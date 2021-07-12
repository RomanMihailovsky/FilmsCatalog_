using FilmsCatalog.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.ViewModels
{
    public class FilmViewModel
    {
        public Film Film { get; set; }
        public Mode Mode { get; set; }
        public int Page { get; set; }
        public IFormFile PosterFile { get; set; }
    }
}
