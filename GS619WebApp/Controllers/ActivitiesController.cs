using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GS619Challenge.Models;
using GS619WebApp.Models;

namespace GS619WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly GS619WebAppContext _context;

        public ActivitiesController(GS619WebAppContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet]
        public IEnumerable<Activity> GetActivity()
        {
            return _context.Activity;
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _context.Activity.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // GET: api/users/898239-239khkjhskdhsd-khkshd/activities
        [HttpGet("~/api/users/{userId}/activities")]
        public async Task<IActionResult> GetActivitiesByUser([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var activities = await GetActivityByUserId(userId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        // GET: api/users/898239-239khkjhskdhsd-khkshd/activities
        [HttpGet("~/api/users/{userId}/activities/top7")]
        public async Task<IActionResult> GetTop7ActivitiesByUser([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var activities = await GetTop7ActivitiesByUserId(userId);

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        // PUT: api/Activities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity([FromRoute] int id, [FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activities
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Activity.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _context.Activity.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();

            return Ok(activity);
        }

        private bool ActivityExists(int id)
        {
            return _context.Activity.Any(e => e.Id == id);
        }

        private async Task<IEnumerable<Activity>> GetActivityByUserId(string userId)
        {
            return await (from a in _context.Activity
                          where a.UserId == userId
                          orderby a.Date descending
                          select a).ToListAsync();
        }

        private async Task<IEnumerable<Activity>> GetTop7ActivitiesByUserId(string userId)
        {
            return await (from a in _context.Activity
                          where a.UserId == userId
                          orderby a.Date descending
                          select a).Take(7).ToListAsync();
        }
    }
}