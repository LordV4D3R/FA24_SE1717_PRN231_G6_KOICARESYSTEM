using KoiCareSys.Data.DTO;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentStageController : ControllerBase
    {
        private IDevelopmenStageSvc _DevelopmentStageSvc;
        public DevelopmentStageController()
        {
            _DevelopmentStageSvc = new DevelopmentStageSvc();
        }

        [HttpGet]
        public async Task<IActionResult> GetAlls()
        {
            var result = await _DevelopmentStageSvc.GetAllDevelopmenStage();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _DevelopmentStageSvc.GetDevelopmenStageById(id);
            return Ok(result.Data);
        }


        // POST api/<JobboardProfileController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DevelopmentStageDTO request)
        {
            var result = await _DevelopmentStageSvc.AddDevelopmenStage(request);
            return Ok(result.Data);
        }

        // PUT api/<JobboardProfileController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] DevelopmentStageUpdateDTO request)
        {
            var result = await _DevelopmentStageSvc.UpdateDevelopmenStage(request);
            return Ok(result.Data);
        }


    }
}
