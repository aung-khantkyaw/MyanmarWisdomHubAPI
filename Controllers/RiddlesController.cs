using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyanmarWisdomHubAPI.Data;
using MyanmarWisdomHubAPI.Models;

namespace MyanmarWisdomHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiddlesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RiddlesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Riddles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Riddle>>> GetRiddle()
        {
            return await _context.Riddle.ToListAsync();
        }

        // GET: api/Riddles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Riddle>> GetRiddle(int id)
        {
            var riddle = await _context.Riddle.FindAsync(id);

            if (riddle == null)
            {
                return NotFound();
            }

            return riddle;
        }

        // PUT: api/Riddles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiddle(int id, Riddle riddle)
        {
            if (id != riddle.Id)
            {
                return BadRequest();
            }

            _context.Entry(riddle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiddleExists(id))
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

        // POST: api/Riddles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Riddle>> PostRiddle(Riddle riddle)
        {
            _context.Riddle.Add(riddle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiddle", new { id = riddle.Id }, riddle);
        }

        // DELETE: api/Riddles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRiddle(int id)
        {
            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle == null)
            {
                return NotFound();
            }

            _context.Riddle.Remove(riddle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RiddleExists(int id)
        {
            return _context.Riddle.Any(e => e.Id == id);
        }
    }
}
