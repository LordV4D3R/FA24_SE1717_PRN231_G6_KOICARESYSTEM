using Microsoft.AspNetCore.Mvc;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;


        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _measurementService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(Guid id)
        {
            return await _measurementService.GetById(id);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> Update(MeasurementDTO request)
        {
            return await _measurementService.Update(request);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody] MeasurementDTO request)
        {
            return await _measurementService.Create(request);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> Delete(Guid id)
        {
            return await _measurementService.DeleteById(id);
        }
    }
}
