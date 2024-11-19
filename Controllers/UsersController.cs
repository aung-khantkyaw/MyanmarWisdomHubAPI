using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users
                    .Select(user => new
                    {
                        Id = user.Id,
                        Username = user.username,
                        Email = user.email,
                        FirstName = user.first_name,
                        LastName = user.last_name,
                        ProfileUrl = user.profile_url
                    })
                    .ToListAsync();

                return users == null || users.Count == 0 ? (ActionResult<IEnumerable<User>>)NotFound("No users found.") : Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            var user = await _context.Users
                                      .Where(u => u.username == username)
                                      .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            return Ok(user); // Return 200 with the user data
        }

        // GET: api/Users/ById/id
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users
                                      .Where(u => u.Id == id)
                                      .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(); // Return 404 if the user is not found
            }

            return Ok(user); // Return 200 with the user data
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> EditUser(string username, [FromBody] UserEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the user by username
            var user = await _context.Users
                                      .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update user properties
            user.email = model.Email ?? user.email;
            user.first_name = model.FirstName ?? user.first_name;
            user.last_name = model.LastName ?? user.last_name;
            user.profile_url = model.profile_url ?? user.profile_url;

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the user.");
            }

            return Ok(new { Message = "User information updated successfully!" });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegisterD)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
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
                var newUser = new User
                {
                    username = userRegisterD.username,
                    email = userRegisterD.email,
                    password = BCrypt.Net.BCrypt.HashPassword(userRegisterD.password),
                    first_name = userRegisterD.first_name,
                    last_name = userRegisterD.last_name,
                    profile_url = userRegisterD.profile_url
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                // Log the exception details here if you have a logging mechanism
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while registering the user", Details = ex.Message });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Query to find the user by username
                var user = await _context.Users
                    .Where(u => u.username == userLogin.username)
                    .Select(u => new
                    {
                        u.Id,
                        u.username,
                        u.email,
                        u.password,
                        u.first_name,
                        u.last_name,
                        u.profile_url
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new { Message = "username is not found!" });
                }

                // Verify the password
                if (BCrypt.Net.BCrypt.Verify(userLogin.password, user.password))
                {
                    return Ok(new
                    {
                        Message = "Login successful",
                        User = new
                        {
                            Id = user.Id,
                            Username = user.username,
                            Email = user.email,
                            FirstName = user.first_name,
                            LastName = user.last_name,
                            ProfileUrl = user.profile_url
                        }
                    });
                }
                else
                {
                    return BadRequest(new { Message = "Invalid password" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here if you have a logging mechanism
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred during login", Details = ex.Message });
            }
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                // Find the user by ID
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                // Remove the user from the context
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception details here if you have a logging mechanism
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while deleting the user", Details = ex.Message });
            }
        }


    }
}
