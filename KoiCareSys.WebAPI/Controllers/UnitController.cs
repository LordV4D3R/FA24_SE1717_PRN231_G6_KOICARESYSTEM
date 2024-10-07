using Microsoft.AspNetCore.Mvc;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Service.Service;
using KoiCareSys.Data.DTO;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService unitService;


        public UnitController()
        {
            unitService = new UnitService();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> getAll()
        {
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDTO>> getById(Guid id)
        {
            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UnitDTO request)
        {
            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitDTO>> Create([FromBody] UnitDTO request)
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
