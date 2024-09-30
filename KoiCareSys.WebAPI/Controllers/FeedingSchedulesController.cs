using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiCareSys.Data;
using KoiCareSys.Data.Models;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedingSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedingSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedingSchedule>>> GetFeedingSchedules()
        {
            return await _context.FeedingSchedules.ToListAsync();
        }

        // GET: api/FeedingSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedingSchedule>> GetFeedingSchedule(Guid id)
        {
            var feedingSchedule = await _context.FeedingSchedules.FindAsync(id);

            if (feedingSchedule == null)
            {
                return NotFound();
            }

            return feedingSchedule;
        }

        // PUT: api/FeedingSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedingSchedule(Guid id, FeedingSchedule feedingSchedule)
        {
            if (id != feedingSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedingSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedingScheduleExists(id))
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

        // POST: api/FeedingSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedingSchedule>> PostFeedingSchedule(FeedingSchedule feedingSchedule)
        {
            _context.FeedingSchedules.Add(feedingSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedingSchedule", new { id = feedingSchedule.Id }, feedingSchedule);
        }

        // DELETE: api/FeedingSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedingSchedule(Guid id)
        {
            var feedingSchedule = await _context.FeedingSchedules.FindAsync(id);
            if (feedingSchedule == null)
            {
                return NotFound();
            }

            _context.FeedingSchedules.Remove(feedingSchedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedingScheduleExists(Guid id)
        {
            return _context.FeedingSchedules.Any(e => e.Id == id);
        }
    }
}
