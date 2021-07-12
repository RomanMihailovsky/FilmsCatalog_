using FilmsCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Data
{
    public class FilmsDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
        : base(options)
        {
        }

    }
}
