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
    public class PostsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PostsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPost()
        {
            var posts = await (from post in _context.Post
                               join user in _context.Users
                               on post.user_id equals user.Id
                               select new
                               {
                                   Profile = user.profile_url,
                                   Username = user.username,
                                   Id = post.Id,
                                   Title = post.title,
                                   Body = post.body
                               }).ToListAsync();

            return Ok(posts);
        }


        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetPosts(int id)
        {
            var posts = await (from post in _context.Post
                               join user in _context.Users
                               on post.user_id equals user.Id
                               where post.user_id == id
                               select new
                               {
                                   Profile = user.profile_url,
                                   Username = user.username,
                                   Id = post.Id,
                                   Title = post.title,
                                   Body = post.body
                               }).ToListAsync();

            if (!posts.Any())
            {
                return NotFound();
            }

            return Ok(posts);
        }




        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
