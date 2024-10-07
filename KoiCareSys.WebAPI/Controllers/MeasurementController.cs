using Microsoft.AspNetCore.Mvc;
using KoiCareSys.Data.Models;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Service.Service;
using KoiCareSys.Data.DTO;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService measurementService;


        public MeasurementController()
        {
            measurementService = new MeasurementService();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> getAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> getById(Guid id)
        {
            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] RegisterNewUserDTO request)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return NoContent();
        }
    }
}
