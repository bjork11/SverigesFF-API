using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigesFF_API.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RatingNumber { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Filmstudio Filmstudio { get; set; }
        public int FilmstudioId { get; set; }
    }
}
