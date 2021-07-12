using FilmsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.ViewModels
{
    public class FilmsViewModel
    {
        public IEnumerable<Film> Films;
        public PageViewModel PageViewModel { get; set; }

    }
}
