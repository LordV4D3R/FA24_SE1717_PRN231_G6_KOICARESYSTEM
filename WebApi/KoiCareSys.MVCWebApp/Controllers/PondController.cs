using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.MVCWebApp.Base;
using KoiCareSys.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class PondController : Controller
    {
        private IApiService _apiService;

        public PondController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name, string note, string description)
        {
            var ponds = new List<PondDto>();
            try
            {
                var queryParams = $"?name={name}&note={note}&description={description}";
                var result = await _apiService.GetAsync<List<PondDto>>($"api/ponds/search{queryParams}");
                if (result != null)
                {
                    ponds = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching ponds: {ex.Message}");
            }

            return View(ponds);
        }

        public async Task<IActionResult> Index()
        {
            var ponds = new List<PondDto>();
            try
            {
                var result = await _apiService.GetAsync<List<PondDto>>("api/ponds");
                if (result != null)
                {
                    ponds = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            return View(ponds);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _apiService.GetAsync<List<UserDto>>("api/users");
            ViewBag.UserId = new SelectList(users, "Id", "Email");
            return View(new PondDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PondDto pond)
        {
            if (!ModelState.IsValid)
            {
                return View(pond);
            }

            try
            {
                var result = await _apiService.PostAsync<BusinessResult>("api/ponds", pond);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Pond.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(pond);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/ponds/{id}");
            if (result != null && result.Status == 1)
            {
                var pond = JsonConvert.DeserializeObject<PondDto>(result.Data.ToString());
                return View(pond);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PondDto pond)
        {
            if (!ModelState.IsValid)
            {
                return View(pond);
            }

            try
            {
                var result = await _apiService.PutAsync<BusinessResult>($"api/ponds/{id}", pond);
                if (result != null && result.Status == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Pond.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return View(pond);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            PondDto pond = null;
            try
            {
                pond = await _apiService.GetAsync<PondDto>($"api/ponds/{id}");

                if (pond == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching pond details: {ex.Message}");
                return BadRequest();
            }

            return View(pond);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var result = await _apiService.GetAsync<BusinessResult>($"api/ponds/{id}");
            if (result != null && result.Status == 1)
            {
                var pond = JsonConvert.DeserializeObject<PondDto>(result.Data.ToString());
                return View(pond);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _apiService.DeleteAsync<BusinessResult>($"api/ponds/{id}");
            if (result != null && result.Status == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting Pond.");
            return View();
        }
    }
}
