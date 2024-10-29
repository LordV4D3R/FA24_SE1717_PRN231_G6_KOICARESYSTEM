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
            //DevelopmentStage developmentStage;
            //Koi koi;
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

                //developmentStage = await _apiService.GetAsync<DevelopmentStage>($"api/DevelopmentStages/{koiRecord.DevelopmentStageId}");
                //koi = await _apiService.GetAsync<Koi>($"api/Kois/{koiRecord.KoiId}");

                //koiRecord.DevelopmentStage = developmentStage;
                //koiRecord.Koi = koi;
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

            var developmentStage = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koi = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStage == null || koi == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.DevelopmentStageId = new SelectList((System.Collections.IEnumerable)developmentStage.Data, "Id", "StageName");
                ViewBag.KoiId = new SelectList((System.Collections.IEnumerable)koi.Data, "Id", "Name");
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
            //if (ModelState.IsValid)
            //{
            //    koiRecord.Id = Guid.NewGuid();
            //    _context.Add(koiRecord);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DevelopmentStageId"] = new SelectList(_context.DevelopmentStages, "Id", "StageName", koiRecord.DevelopmentStageId);
            //ViewData["KoiId"] = new SelectList(_context.Kois, "Id", "Name", koiRecord.KoiId);
            //return View(koiRecord);
            if (ModelState.IsValid)
            {
                try
                {
                    await _apiService.PostAsync<BusinessResult>("api/KoiRecord", koiRecord);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating record: {ex.Message}");
                    return StatusCode(500);
                }
            }


            var developmentStage = await _apiService.GetAsync<BusinessResult>($"api/DevelopmentStage");
            var koi = await _apiService.GetAsync<BusinessResult>($"api/Kois");
            if (developmentStage == null || koi == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.DevelopmentStageId = new SelectList((System.Collections.IEnumerable)developmentStage.Data, "Id", "StageName");
                ViewBag.KoiId = new SelectList((System.Collections.IEnumerable)koi, "Id", "Name");
            }
            return View(koiRecord);
        }

        // GET: KoiRecords/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var koiRecord = await _context.KoiRecords.FindAsync(id);
            //if (koiRecord == null)
            //{
            //    return NotFound();
            //}
            //ViewData["DevelopmentStageId"] = new SelectList(_context.DevelopmentStages, "Id", "StageName", koiRecord.DevelopmentStageId);
            //ViewData["KoiId"] = new SelectList(_context.Kois, "Id", "Name", koiRecord.KoiId);
            //return View(koiRecord);
            if (id == null)
            {
                return NotFound();
            }

            KoiRecord koiRecord;
            try
            {
                var response = await _apiService.GetAsync<BusinessResult>($"api/KoiRecord/{id}");
                koiRecord = (KoiRecord)response.Data;
                if (koiRecord == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching edit data: {ex.Message}");
                return StatusCode(500);
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
            //if (id != koiRecord.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(koiRecord);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!KoiRecordExists(koiRecord.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DevelopmentStageId"] = new SelectList(_context.DevelopmentStages, "Id", "StageName", koiRecord.DevelopmentStageId);
            //ViewData["KoiId"] = new SelectList(_context.Kois, "Id", "Name", koiRecord.KoiId);
            //return View(koiRecord);
            if (id != koiRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiService.PutAsync<BusinessResult>("api/KoiRecord", koiRecord);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating record: {ex.Message}");
                    return StatusCode(500);
                }
            }

            return View(koiRecord);
        }

        // GET: KoiRecords/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //KoiRecord koiRecord;
            //try
            //{
            //    var response = await _apiService.GetAsync<BusinessResult>($"api/KoiRecords/{id}");
            //    koiRecord = (KoiRecord)response.Data;
            //    if (koiRecord == null)
            //    {
            //        return NotFound();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error fetching delete data: {ex.Message}");
            //    return StatusCode(500);
            //}

            //return View(koiRecords);
            throw new NotImplementedException();
        }

        // POST: KoiRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var koiRecord = await _context.KoiRecords.FindAsync(id);
            //if (koiRecord != null)
            //{
            //    _context.KoiRecords.Remove(koiRecord);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            throw new NotImplementedException();
        }

        private bool KoiRecordExists(Guid id)
        {
            //return _context.KoiRecords.Any(e => e.Id == id);
            throw new NotImplementedException();
        }
    }
}
