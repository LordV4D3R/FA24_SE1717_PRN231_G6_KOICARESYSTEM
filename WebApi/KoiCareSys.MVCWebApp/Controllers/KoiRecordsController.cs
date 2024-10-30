using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiCareSys.Data;
using KoiCareSys.Data.Models;
using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.MVCWebApp.ApiService;
using KoiCareSys.MVCWebApp.Base;
using Newtonsoft.Json;
using KoiCareSys.Data.DTO;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class KoiRecordsController : Controller
    {
        private IApiService _apiService;

        public KoiRecordsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: KoiRecords
        public async Task<IActionResult> Index()
        {
            var datas = new List<KoiRecord>();
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/KoiRecord");
                if (result != null && result.Status == 1)
                {
                    datas = JsonConvert.DeserializeObject<List<KoiRecord>>(result.Data.ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching koi records: {ex.Message}");
            }

            return View(datas);
        }

        // GET: KoiRecords/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            KoiRecord koiRecord;
            try
            {
                var resultKoiRecord = await _apiService.GetAsync<BusinessResult>($"api/KoiRecord/{id}");
                if (resultKoiRecord != null && resultKoiRecord.Status == 1)
                {
                    koiRecord = JsonConvert.DeserializeObject<KoiRecord>(resultKoiRecord.Data.ToString());
                }
                else
                {
                    return NotFound();
                }
                var resultDevelopmentStage = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage/{koiRecord.DevelopmentStageId}");
                if (resultDevelopmentStage != null && resultDevelopmentStage.Status == 1)
                {
                    koiRecord.DevelopmentStage = JsonConvert.DeserializeObject<DevelopmentStage>(resultDevelopmentStage.Data.ToString());
                }
                else
                {
                    return NotFound();
                }
                var resultKoi = await _apiService.GetAsync<BusinessResult>($"api/Kois/{koiRecord.KoiId}");
                if (resultKoi != null && resultKoi.Status == 1)
                {
                    koiRecord.Koi = JsonConvert.DeserializeObject<Koi>(resultKoi.Data.ToString());
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching details: {ex.Message}");
                return StatusCode(500);
            }

            return View(koiRecord);


        }

        // GET: KoiRecords/Create
        public async Task<IActionResult> Create()
        {

            var developmentStageResult = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koiResult = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStageResult.Status == 1 && koiResult.Status == 1)
            {
                List<DevelopmentStage> developmentStages = JsonConvert.DeserializeObject<List<DevelopmentStage>>(developmentStageResult.Data.ToString());
                List<Koi> kois = JsonConvert.DeserializeObject<List<Koi>>(koiResult.Data.ToString());
                ViewBag.DevelopmentStageId = new SelectList(developmentStages, "Id", "StageName");
                ViewBag.KoiId = new SelectList(kois, "Id", "Name");
            }
            else
            {
                return NotFound();
            }

            return View();
        }

        // POST: KoiRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RecordName,Color,Price,Description,HealthIssue,Weight,RecordAt,Length,Physique,KoiId,DevelopmentStageId")] KoiRecord koiRecord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    KoiRecordDTO koiRecordData = new KoiRecordDTO()
                    {
                        KoiId = koiRecord.KoiId,
                        Physique = koiRecord.Physique,
                        Length = (decimal)koiRecord.Length,
                        Weight = (decimal)koiRecord.Weight,
                        Color = koiRecord.Color,
                        Description = koiRecord.Description,
                        HealthIssue = koiRecord.HealthIssue,
                        Price = koiRecord.Price,
                        RecordName = koiRecord.RecordName,
                        RecordAt = koiRecord.RecordAt,
                        DevelopmentStageId = koiRecord.DevelopmentStageId
                    };

                    await _apiService.PostAsync<BusinessResult>("api/KoiRecord", koiRecordData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating record: {ex.Message}");
                    return StatusCode(500);
                }
            }


            var developmentStageResult = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koiResult = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStageResult.Status == 1 && koiResult.Status == 1)
            {
                List<DevelopmentStage> developmentStages = JsonConvert.DeserializeObject<List<DevelopmentStage>>(developmentStageResult.Data.ToString());
                List<Koi> kois = JsonConvert.DeserializeObject<List<Koi>>(koiResult.Data.ToString());
                ViewBag.DevelopmentStageId = new SelectList(developmentStages, "Id", "StageName");
                ViewBag.KoiId = new SelectList(kois, "Id", "Name");
            }
            else
            {
                return NotFound();
            }
            return View(koiRecord);
        }

        // GET: KoiRecords/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KoiRecord koiRecord;
            try
            {
                var resultKoiRecord = await _apiService.GetAsync<BusinessResult>($"api/KoiRecord/{id}");
                koiRecord = JsonConvert.DeserializeObject<KoiRecord>(resultKoiRecord.Data.ToString());

                var resultDevelopmentStage = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage/{koiRecord.DevelopmentStageId}");
                koiRecord.DevelopmentStage = JsonConvert.DeserializeObject<DevelopmentStage>(resultDevelopmentStage.Data.ToString());

                var resultKoi = await _apiService.GetAsync<BusinessResult>($"api/Kois/{koiRecord.KoiId}");
                koiRecord.Koi = JsonConvert.DeserializeObject<Koi>(resultKoi.Data.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching edit data: {ex.Message}");
                return StatusCode(500);
            }

            var developmentStageResult = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koiResult = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStageResult.Status == 1 && koiResult.Status == 1)
            {
                List<DevelopmentStage> developmentStages = JsonConvert.DeserializeObject<List<DevelopmentStage>>(developmentStageResult.Data.ToString());
                List<Koi> kois = JsonConvert.DeserializeObject<List<Koi>>(koiResult.Data.ToString());
                ViewBag.DevelopmentStageId = new SelectList(developmentStages, "Id", "StageName");
                ViewBag.KoiId = new SelectList(kois, "Id", "Name");
            }
            else
            {
                return NotFound();
            }
            return View(koiRecord);
        }

        // POST: KoiRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RecordName,Color,Price,Description,HealthIssue,Weight,RecordAt,Length,Physique,KoiId,DevelopmentStageId")] KoiRecord koiRecord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    KoiRecordUpdateDTO koiRecordData = new KoiRecordUpdateDTO()
                    {
                        Id = koiRecord.Id,
                        KoiId = koiRecord.KoiId,
                        Physique = koiRecord.Physique,
                        Length = (decimal)koiRecord.Length,
                        Weight = (decimal)koiRecord.Weight,
                        Color = koiRecord.Color,
                        Description = koiRecord.Description,
                        HealthIssue = koiRecord.HealthIssue,
                        Price = koiRecord.Price,
                        RecordName = koiRecord.RecordName,
                        RecordAt = koiRecord.RecordAt,
                        DevelopmentStageId = koiRecord.DevelopmentStageId
                    };

                    await _apiService.PutAsync<BusinessResult>("api/KoiRecord", koiRecordData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating record: {ex.Message}");
                    return StatusCode(500);
                }
            }


            var developmentStageResult = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koiResult = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStageResult.Status == 1 && koiResult.Status == 1)
            {
                List<DevelopmentStage> developmentStages = JsonConvert.DeserializeObject<List<DevelopmentStage>>(developmentStageResult.Data.ToString());
                List<Koi> kois = JsonConvert.DeserializeObject<List<Koi>>(koiResult.Data.ToString());
                ViewBag.DevelopmentStageId = new SelectList(developmentStages, "Id", "StageName");
                ViewBag.KoiId = new SelectList(kois, "Id", "Name");
            }
            else
            {
                return NotFound();
            }
            return View(koiRecord);
        }

        // GET: KoiRecords/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KoiRecord koiRecord;
            try
            {
                var resultKoiRecord = await _apiService.GetAsync<BusinessResult>($"api/KoiRecord/{id}");
                koiRecord = JsonConvert.DeserializeObject<KoiRecord>(resultKoiRecord.Data.ToString());

                var resultDevelopmentStage = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage/{koiRecord.DevelopmentStageId}");
                koiRecord.DevelopmentStage = JsonConvert.DeserializeObject<DevelopmentStage>(resultDevelopmentStage.Data.ToString());

                var resultKoi = await _apiService.GetAsync<BusinessResult>($"api/Kois/{koiRecord.KoiId}");
                koiRecord.Koi = JsonConvert.DeserializeObject<Koi>(resultKoi.Data.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching edit data: {ex.Message}");
                return StatusCode(500);
            }

            var developmentStageResult = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koiResult = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStageResult.Status == 1 && koiResult.Status == 1)
            {
                List<DevelopmentStage> developmentStages = JsonConvert.DeserializeObject<List<DevelopmentStage>>(developmentStageResult.Data.ToString());
                List<Koi> kois = JsonConvert.DeserializeObject<List<Koi>>(koiResult.Data.ToString());
                ViewBag.DevelopmentStageId = new SelectList(developmentStages, "Id", "StageName");
                ViewBag.KoiId = new SelectList(kois, "Id", "Name");
            }
            else
            {
                return NotFound();
            }
            return View(koiRecord);
        }

        // POST: KoiRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await _apiService.DeleteAsync<BusinessResult>($"api/KoiRecord/{id}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting record: {ex.Message}");
                return StatusCode(500);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
