using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigesFF_API.Models
{
    public class Trivia
    {
        public int Id { get; set; }
        public string TriviaText { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Filmstudio Filmstudio { get; set; }
        public int FilmstudioId { get; set; }
    }
}
