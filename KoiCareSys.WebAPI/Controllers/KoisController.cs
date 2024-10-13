using KoiCareSys.Data.DTO;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoisController : ControllerBase
    {
        private readonly IKoiService _koiService;

        public KoisController(IKoiService koiService)
        {
            _koiService = koiService;
        }

        [HttpGet]
        public async Task<IBusinessResult> GetAll()
        {
            return await _koiService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetById(string id)
        {
            return await _koiService.GetById(id);
        }

        //[HttpPut("{id}")]
        //public async Task<IBusinessResult> Update(MeasurementDTO request)
        //{
        //    return await _koiService.Save(request);
        //}

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody] AddNewKoiDTO request)
        {
            return await _koiService.Save(request);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> Delete(string id)
        {
            return await _koiService.DeleteById(id);
        }
    }
}
