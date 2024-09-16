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
    public class NotificationsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public NotificationsController(MyDbContext context)
        {
            _context = context;
        }

        //GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotification()
        {
            return await _context.Notification.ToListAsync();
        }


        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var Notification = await _context.Notification.FindAsync(id);

            if (Notification == null)
            {
                return NotFound();
            }

            return Notification;
        }

        // GET: api/Notifications/username
        [HttpGet("username/{username}")]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotificationsByUsername(string username)
        {
            var notifications = await _context.Notification
                .Where(n => n.to_user_name == username) // Assuming there's a Username property in Notification
                .OrderByDescending(n => n.Id)
                .Take(5)
                .ToListAsync();

            if (notifications == null || notifications.Count == 0)
            {
                return NotFound();
            }

            return Ok(notifications);
        }


        //// PUT: api/Notifications/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutNotification(int id, Notification Notification)
        //{
        //    if (id != Notification.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Notification).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!NotificationExists(id))
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

        // PUT: api/Notifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id)
        {

            // Find the existing entity
            var notification = await _context.Notification.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            // Update only the specific field
            notification.is_answer = true;

            // Mark the entity as modified
            _context.Entry(notification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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


        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification Notification)
        {
            _context.Notification.Add(Notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotification", new { id = Notification.Id }, Notification);
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var Notification = await _context.Notification.FindAsync(id);
            if (Notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(Notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificationExists(int id)
        {
            return _context.Notification.Any(e => e.Id == id);
        }
    }
}
