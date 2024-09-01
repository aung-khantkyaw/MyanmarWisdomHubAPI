using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using MyanmarWisdomHubAPI.Data;
using MyanmarWisdomHubAPI.Models.User;

namespace MyanmarWisdomHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUser([FromBody] UserEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the current user (you might need to implement your own method to get the user)
            var user = await _context.Users.FindAsync(model.Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user information
            user.username = model.Username;
            user.email = model.Email;
            user.first_name = model.FirstName;
            user.last_name = model.LastName;

            // Save changes to the database
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User information updated successfully!" });
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegisterD)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists
                bool usernameExists = await _context.Users
                    .AnyAsync(u => u.username == userRegisterD.username);

                if (usernameExists)
                {
                    return BadRequest("Username is already taken.");
                }

                // Check if the email already exists
                bool emailExists = await _context.Users
                    .AnyAsync(u => u.email == userRegisterD.email);

                if (emailExists)
                {
                    return BadRequest("Email is already taken.");
                }

                // Create and save the new user
                var user = new User
                {
                    username = userRegisterD.username,
                    email = userRegisterD.email,
                    password = BCrypt.Net.BCrypt.HashPassword(userRegisterD.password),
                    first_name = userRegisterD.first_name,
                    last_name = userRegisterD.last_name
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Registration successful" });
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.username == userLogin.username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.password, user.password))
            {
                return BadRequest(new { Message = "Invalid username or password" });
            }

            return Ok(new { Message = "Login successful", User = new { user.Id, user.username, user.email } });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
