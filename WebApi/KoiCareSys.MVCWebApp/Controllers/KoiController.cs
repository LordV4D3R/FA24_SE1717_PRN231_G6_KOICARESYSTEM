using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.MVCWebApp.Base;
using KoiCareSys.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class KoiController : Controller
    {
        private IApiService _apiService;

        public KoiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name, string category, string origin)
        {
            var kois = new List<KoiDto>();
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/kois");
                if (result != null && result.Status == 1)
                {
                    kois = JsonConvert.DeserializeObject<List<KoiDto>>(result.Data.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            var filteredKois = kois
                .Where(k => (string.IsNullOrEmpty(name) || k.Name.ToLower().Contains(name.ToLower())) &&
                            (string.IsNullOrEmpty(category) || k.Category.ToLower().Contains(category.ToLower())) &&
                            (string.IsNullOrEmpty(origin) || k.Origin.ToLower().Contains(origin.ToLower())))
                .ToList();
            return View("Index", filteredKois);
        }

        public async Task<IActionResult> Index()
        {
            var kois = new List<KoiDto>();
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/kois");
                if (result != null && result.Status == 1)
                {
                    kois = JsonConvert.DeserializeObject<List<KoiDto>>(result.Data.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            return View(kois);
        }

        public async Task<IActionResult> Create()
        {
            var ponds = await _apiService.GetAsync<List<PondDto>>("api/pond");
            ViewBag.PondId = new SelectList(ponds, "Id", "PondName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KoiDto koi)
        {
            if (!ModelState.IsValid)
            {
                return View(koi);
            }

            try
            {
                var result = await _apiService.PostAsync<BusinessResult>("api/kois", koi);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Koi.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(koi);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/kois/{id}");
            if (result != null && result.Status == 1)
            {
                var koi = JsonConvert.DeserializeObject<KoiDto>(result.Data.ToString());
                return View(koi);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, KoiDto koi)
        {
            if (!ModelState.IsValid)
            {
                return View(koi);
            }

            try
            {
                var result = await _apiService.PutAsync<BusinessResult>($"api/kois/{id}", koi);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Koi.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(koi);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/kois/{id}");
            if (result != null && result.Status == 1)
            {
                var koi = JsonConvert.DeserializeObject<KoiDto>(result.Data.ToString());
                return View(koi);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/kois/{id}");
            if (result != null && result.Status == 1)
            {
                var koi = JsonConvert.DeserializeObject<KoiDto>(result.Data.ToString());
                return View(koi);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _apiService.DeleteAsync<BusinessResult>($"api/kois/{id}");
            if (result != null && result.Status == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting Koi.");
            return View();
        }
    }
}
