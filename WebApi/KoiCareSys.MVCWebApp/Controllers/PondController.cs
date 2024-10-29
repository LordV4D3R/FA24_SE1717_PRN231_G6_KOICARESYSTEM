using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.MVCWebApp.Base;
using KoiCareSys.MVCWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class PondController : Controller
    {
        private IApiService _apiService;
        private readonly IConfiguration _configuration;

        public PondController(IApiService apiService, IConfiguration configuration)
        {
            _apiService = apiService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name, string note, string description)
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

            var filteredPonds = ponds
                .Where(p => (string.IsNullOrEmpty(name) || p.PondName.ToLower().Contains(name.ToLower())) &&
                            (string.IsNullOrEmpty(note) || p.Note.ToLower().Contains(note.ToLower())) &&
                            (string.IsNullOrEmpty(description) || p.Description.ToLower().Contains(description.ToLower())))
                .ToList();

            return View("Index", filteredPonds);
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

        [HttpGet("PondAjax")]
        public async Task<IActionResult> PondAjax()
        {
            var ponds = new List<PondDto>();
            try
            {
                var result = await _apiService.GetAsync<List<PondDto>>("api/ponds");
                if (result != null && result.Any())
                {
                    ponds = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            // Return the view with the list of ponds
            return View(ponds);
        }

        public async Task<IActionResult> Map()
        {
            var ponds = await _apiService.GetAsync<List<PondDto>>("api/ponds");
            ViewBag.GoogleMapsApiKey = _configuration["GoogleMaps:ApiKey"];
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
                var users = await _apiService.GetAsync<List<UserDto>>("api/users");
                ViewBag.UserId = new SelectList(users, "Id", "Email");
                return View("Create", pond);
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
                    ModelState.AddModelError("", "Pond name already exists.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
            }

            var usersOnError = await _apiService.GetAsync<List<UserDto>>("api/users");
            ViewBag.UserId = new SelectList(usersOnError, "Id", "Email");
            return View("Create", pond);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var ponds = await _apiService.GetAsync<PondDto>($"api/ponds/{id}");
            var users = await _apiService.GetAsync<List<UserDto>>("api/users");
            ViewBag.UserId = new SelectList(users, "Id", "Email");

            return View(ponds);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PondDto pond)
        {
            if (!ModelState.IsValid)
            {
                var users = await _apiService.GetAsync<List<UserDto>>("api/users");
                ViewBag.UserId = new SelectList(users, "Id", "Email");
                return View("Edit", pond);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred: " + ex.Message);
            }

            var usersOnError = await _apiService.GetAsync<List<UserDto>>("api/users");
            ViewBag.UserId = new SelectList(usersOnError, "Id", "Email");
            return View("Edit", pond);
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

            var result = await _apiService.GetAsync<PondDto>($"api/ponds/{id}");
            if (result != null)
            {
                return View(result);
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
