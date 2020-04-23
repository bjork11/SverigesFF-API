using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SverigesFF_API.Models;

namespace SverigesFF_API.Models
{
    public class MovieRentalContext : DbContext
    {
        public MovieRentalContext(DbContextOptions<MovieRentalContext> options) : base(options){ }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Filmstudio> Filmstudio { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
    }
}
