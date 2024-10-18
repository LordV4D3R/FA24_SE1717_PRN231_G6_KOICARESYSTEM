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
        public async Task<IBusinessResult> GetById(Guid id)
        {
            return await _koiService.GetById(id);
        }

        [HttpPut("{id}")]
        public async Task<IBusinessResult> Update(Guid id, [FromBody] KoiDTO request)
        {
            return await _koiService.Save(id, request);
        }

        [HttpPost]
        public async Task<IBusinessResult> Create([FromBody] KoiDTO request)
        {
            return await _koiService.Save(dto: request);
        }

        [HttpDelete("{id}")]
        public async Task<IBusinessResult> Delete(Guid id)
        {
            return await _koiService.DeleteById(id);
        }
    }
}
