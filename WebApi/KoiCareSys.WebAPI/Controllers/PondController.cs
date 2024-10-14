using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PondController : ControllerBase
    {
        private readonly IPondService _pondService;

        public PondController(IPondService pondService)
        {
            _pondService = pondService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _pondService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(Guid id)
        {
            return await _pondService.GetById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> Update(PondDTO request)
        {
            return await _pondService.Update(request);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody] PondDTO request)
        {
            return await _pondService.Create(request);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> Delete(Guid id)
        {
            return await _pondService.DeleteById(id);
        }
    }
}
