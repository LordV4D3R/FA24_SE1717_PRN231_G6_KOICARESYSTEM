using Azure.Core;
using KoiCareSys.Common;
using KoiCareSys.Data;
using KoiCareSys.Data.DTO;
using KoiCareSys.Data.Models;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedingSchedulesController : ControllerBase
    {
        private readonly IFeedingScheduleService _feedingScheduleService;

        public FeedingSchedulesController(IFeedingScheduleService feedingScheduleService)
        {
            _feedingScheduleService = feedingScheduleService;
        }

        // GET: api/FeedingSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedingSchedule>>> GetFeedingSchedules([FromQuery] string? search)
        {
            try
            {
                var result = await _feedingScheduleService.GetAll(search);
                if (result.Status > 0)
                {
                    return Ok(result.Data as IEnumerable<FeedingSchedule>);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/FeedingSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedingSchedule>> GetFeedingSchedule(Guid id)
        {
            try
            {
                var result = await _feedingScheduleService.GetById(id);
                if (result.Status > 0)
                {
                    return Ok(result.Data as FeedingSchedule);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/FeedingSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedingSchedule(Guid id, FeedingScheduleDTO feedingSchedule)
        {
            try
            {
                var result = await _feedingScheduleService.GetById(id);
                if (result.Status < 0)
                {
                    return BadRequest();
                }
                var feedingScheduleUpdate = result.Data as FeedingSchedule;
                var update = await _feedingScheduleService.Update(feedingScheduleUpdate, feedingSchedule);
                if (update.Status > 0)
                {
                    return Ok(update.Data as FeedingSchedule);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/FeedingSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedingSchedule>> PostFeedingSchedule([FromBody] FeedingScheduleDTO feedingSchedule)
        {
            try
            {
                var newSchedule = new FeedingScheduleDTO
                {
                    FeedAt = feedingSchedule.FeedAt,
                    FoodAmount = feedingSchedule.FoodAmount,
                    FoodType = feedingSchedule.FoodType,
                    Note = feedingSchedule.Note,
                    PondId = feedingSchedule.PondId

                };
                var result = await _feedingScheduleService.Create(newSchedule);
                if (result.Status > 0)
                {
                    return Ok(result.Data as FeedingSchedule);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/FeedingSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedingSchedule(Guid id)
        {
            try
            {
                var result = await _feedingScheduleService.GetById(id);
                if (result.Status < 0)
                {
                    return BadRequest();
                }
                var delete = await _feedingScheduleService.Delete(id);
                if (delete.Status > 0)
                {
                    return Ok();
                }
                else { return NotFound(result.Message); }

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CalculateFoodAmount")]
        public async Task<ActionResult<decimal>> CalculateFoodAmount([FromBody] Guid pondId)
        {
            try
            {
                if (pondId == Guid.Empty)
                {
                    return BadRequest("Invalid pondId");
                }

                var result = await _feedingScheduleService.CaculateFoodAmountByKoi(pondId);

                if (result.Status > 0)  // Status > 0 có nghĩa là thành công
                {
                    return Ok(result.Data);  // `result.Data` chứa tổng lượng thức ăn
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
