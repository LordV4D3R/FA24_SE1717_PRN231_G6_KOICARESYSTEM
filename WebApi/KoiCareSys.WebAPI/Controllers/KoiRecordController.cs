﻿using KoiCareSys.Data.DTO;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiRecordController : ControllerBase
    {
        private readonly IKoiRecordSvc _koiRecordSvc;

        public KoiRecordController(IKoiRecordSvc koiRecordSvc)
        {
            _koiRecordSvc = koiRecordSvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKoiRecordsAsync()
        {
            var result = await _koiRecordSvc.GetAllKoiRecordsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoiRecordByIdAsync(Guid id)
        {
            var result = await _koiRecordSvc.GetKoiRecordByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddKoiRecordAsync([FromBody] KoiRecordDTO request)
        {
            var result = await _koiRecordSvc.AddKoiRecordAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateKoiRecordAsync([FromBody] KoiRecordUpdateDTO request)
        {
            var result = await _koiRecordSvc.UpdateKoiRecordAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveKoiRecord(Guid id)
        {
            var result = await _koiRecordSvc.RemoveKoiRecord(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var result = await _koiRecordSvc.GetAllKoiRecordsByKeywordAsync(keyword);
            return Ok(result);
        }
    }
}
