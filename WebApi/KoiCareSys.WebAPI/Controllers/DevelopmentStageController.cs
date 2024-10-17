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
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _DevelopmentStageSvc.GetDevelopmenStageById(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DevelopmentStageDTO request)
        {
            var result = await _DevelopmentStageSvc.AddDevelopmenStage(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DevelopmentStageUpdateDTO request)
        {
            var result = await _DevelopmentStageSvc.UpdateDevelopmenStage(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _DevelopmentStageSvc.DeleteDevelopmenStage(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var result = await _DevelopmentStageSvc.GetAllDevelopmenStageByKeyword(keyword);
            return Ok(result);
        }

    }
}
