using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SverigesFF_API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuantityForRent { get; set; }

        public void AdjustQtyOnMovieRental()
        {
            this.QuantityForRent--;
        }

        public void AdjustQtyOnMovieReturn()
        {
            this.QuantityForRent++;
        }

        public bool CheckIfAvailable()
        {
            if (QuantityForRent > 0)
            {
                return true;
            }

            return false;
        }
    }
}
