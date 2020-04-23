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
    public class TriviasController : ControllerBase
    {
        private readonly MovieRentalContext _context;

        public TriviasController(MovieRentalContext context)
        {
            _context = context;
        }

        // GET: api/Trivias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trivia>>> GetTrivia()
        {
            return await _context.Trivias.Include(x => x.Movie).Include(x => x.Filmstudio).ToListAsync();
        }

        // GET: api/Trivias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trivia>> GetTrivia(int id)
        {
            var trivia = await _context.Trivias.Where(m => m.Id == id).Include(m => m.Movie).Include(f => f.Filmstudio).FirstOrDefaultAsync();

            if (trivia == null)
            {
                return NotFound();
            }

            return trivia;
        }

        // PUT: api/Trivias/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrivia(int id, Trivia trivia)
        {
            if (id != trivia.Id)
            {
                return BadRequest();
            }

            _context.Entry(trivia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaExists(id))
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

        // POST: api/Trivias
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Trivia>> PostTrivia(Trivia trivia)
        {
            _context.Trivias.Add(trivia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrivia", new { id = trivia.Id }, trivia);
        }

        // DELETE: api/Trivias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trivia>> DeleteTrivia(int id)
        {
            var trivia = await _context.Trivias.FindAsync(id);
            if (trivia == null)
            {
                return NotFound();
            }

            _context.Trivias.Remove(trivia);
            await _context.SaveChangesAsync();

            return trivia;
        }

        private bool TriviaExists(int id)
        {
            return _context.Trivias.Any(e => e.Id == id);
        }
    }
}
