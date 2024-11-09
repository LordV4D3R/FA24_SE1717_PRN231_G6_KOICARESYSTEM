using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.MVCWebApp.Base;
using KoiCareSys.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly IApiService _apiService;

        public UnitController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var units = new List<UnitDto>();
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/Unit");
                if (result != null && result.Status == 1)
                {
                    units = JsonConvert.DeserializeObject<List<UnitDto>>(result.Data.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching units: {ex.Message}");
            }

            return View(units);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitDto unit)
        {
            if (!ModelState.IsValid)
            {
                return View(unit);
            }

            try
            {
                var result = await _apiService.PostAsync<BusinessResult>("api/Unit", unit);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the unit.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(unit);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/Unit/{id}");
            if (result != null && result.Status == 1)
            {
                var unit = JsonConvert.DeserializeObject<UnitDto>(result.Data.ToString());
                return View(unit);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UnitDto unit)
        {
            if (!ModelState.IsValid)
            {
                return View(unit);
            }

            try
            {
                var result = await _apiService.PutAsync<BusinessResult>($"api/Unit/{id}", unit);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the unit.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(unit);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/Unit/{id}");
            if (result != null && result.Status == 1)
            {
                var unit = JsonConvert.DeserializeObject<UnitDto>(result.Data.ToString());
                return View(unit);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _apiService.DeleteAsync<BusinessResult>($"api/Unit/{id}");
            if (result != null && result.Status == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting unit.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/Unit/{id}");
            if (result != null && result.Status == 1)
            {
                var unit = JsonConvert.DeserializeObject<UnitDto>(result.Data.ToString());
                return View(unit);
            }

            return NotFound();
        }   
    }
}

