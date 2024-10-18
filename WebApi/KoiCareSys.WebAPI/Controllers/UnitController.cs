using Microsoft.AspNetCore.Mvc;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;


        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _unitService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(Guid id)
        {
            return await _unitService.GetById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> Update(UnitDTO request)
        {
            return await _unitService.Update(request);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody] UnitDTO request)
        {
            return await _unitService.Create(request);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> Delete(Guid id)
        {
            return await _unitService.DeleteById(id);
        }
        
    }
}
