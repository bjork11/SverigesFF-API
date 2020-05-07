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
    public class RentalsController : ControllerBase
    {
        private readonly MovieRentalContext _context;

        public RentalsController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: api/Rentals
        // List of all rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRental()
        {
            return await _context.Rentals.Include(m => m.Movie).Include(f => f.Filmstudio).ToListAsync();
        }

        // GET: api/Rentals/5
        // Info about a specific rental ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.Where(r => r.Id == id).Include(m => m.Movie).Include(f => f.Filmstudio).FirstOrDefaultAsync();

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        // GET: api/Rentals/StudioId/1
        // List of all rentals by studio ID
        [HttpGet("StudioId/{id}")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalForStudioById(int id)
        {
            return await _context.Rentals.Include(m => m.Movie).Include(f => f.Filmstudio).ToListAsync();
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRental(int id, Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rental>> PostRental(Rental rental)
        {
            rental.Movie = await _context.Movies.Where(x => x.Id == rental.MovieId).FirstOrDefaultAsync();

            if (rental.Movie.CheckIfAvailable())
            {
                rental.Movie.AdjustQtyOnMovieRental();

                _context.Rentals.Add(rental);
                _context.Entry(rental.Movie).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRental", new { id = rental.Id }, rental);
            }

            throw new InvalidOperationException("Filmen finns inte tillgänglig för uthyrning!");
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rental>> DeleteRental(int id)
        {
            var rental = await _context.Rentals.Where(i => i.Id == id).Include(m => m.Movie).Include(f => f.Filmstudio).FirstOrDefaultAsync();
            if (rental == null)
            {
                return NotFound();
            }

            var movie = rental.Movie;
            movie.AdjustQtyOnMovieReturn();

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
