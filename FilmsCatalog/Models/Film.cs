using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models
{

    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int? Year { get; set; }
        public string Producer { get; set; }
        public byte[] Poster { get; set; }
        public string PosterMimeType { get; set; }
        public string UserName { get; set; }

    }
}
