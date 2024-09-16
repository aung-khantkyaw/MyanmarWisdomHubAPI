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
    public class Attempt_QuizsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public Attempt_QuizsController(MyDbContext context)
        {
            _context = context;
        }

        //GET: api/Attempt_Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attempt_Quiz>>> GetAttempt_Quiz()
        {
            try
            {
                var attemptQuizzes = await _context.Attempt_Quiz.ToListAsync();
                if (attemptQuizzes == null || !attemptQuizzes.Any())
                {
                    return NotFound(); 
                }
                return Ok(attemptQuizzes); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error"); 
            }
        }

        // GET: api/Attempt_Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attempt_Quiz>> GetAttempt_Quiz(int id)
        {
            var Attempt_Quiz = await _context.Attempt_Quiz.FindAsync(id);

            if (Attempt_Quiz == null)
            {
                return NotFound();
            }

            return Attempt_Quiz;
        }

        //// PUT: api/Attempt_Quizs/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutAttempt_Quiz(int id, Attempt_Quiz Attempt_Quiz)
        //{
        //    if (id != Attempt_Quiz.attempt_Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Attempt_Quiz).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!Attempt_QuizExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        public class UpdateAttemptQuizModel
        {
            public int PlayerTwoScore { get; set; }
        }

        // PUT: api/Attempt_Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttempt_Quiz(int id, [FromBody] UpdateAttemptQuizModel updateModel)
        {
            if (updateModel == null || id <= 0)
            {
                return BadRequest();
            }

            // Find the existing entity
            var attemptQuiz = await _context.Attempt_Quiz.FindAsync(id);
            if (attemptQuiz == null)
            {
                return NotFound();
            }

            // Update only the specific field
            attemptQuiz.player_two_score = updateModel.PlayerTwoScore;

            // Mark the entity as modified
            _context.Entry(attemptQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Attempt_QuizExists(id))
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


        // POST: api/Attempt_Quizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attempt_Quiz>> PostAttempt_Quiz(Attempt_Quiz Attempt_Quiz)
        {
            _context.Attempt_Quiz.Add(Attempt_Quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttempt_Quiz", new { id = Attempt_Quiz.attempt_Id }, Attempt_Quiz);
        }

        // DELETE: api/Attempt_Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttempt_Quiz(int id)
        {
            var Attempt_Quiz = await _context.Attempt_Quiz.FindAsync(id);
            if (Attempt_Quiz == null)
            {
                return NotFound();
            }

            _context.Attempt_Quiz.Remove(Attempt_Quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Attempt_QuizExists(int id)
        {
            return _context.Attempt_Quiz.Any(e => e.attempt_Id == id);
        }
    }
}
