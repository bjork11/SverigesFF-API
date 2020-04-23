using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SverigesFF_API.Models;

namespace SverigesFF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelXMLsController : ControllerBase
    {
        private readonly MovieRentalContext _context;

        public LabelXMLsController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: api/LabelXMLs/1
        [HttpGet("{id}")]
        [Produces("application/xml")]
        public async Task<ActionResult<LabelXML>>GetLabelXML(int id)
        {
            var rental = await _context.Rentals.Where(r => r.Id == id).Include(m => m.Movie).Include(f => f.Filmstudio).FirstOrDefaultAsync();

            if (rental == null)
            {
                return NotFound();
            }

            var label = new LabelXML { Title = rental.Movie.Title, RentalStart = rental.RentalDate, Location = rental.Filmstudio.Location };

            return label;
        }
    }
}
