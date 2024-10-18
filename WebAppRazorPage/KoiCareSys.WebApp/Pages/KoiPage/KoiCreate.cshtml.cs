using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Base;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KoiCareSys.WebApp.Pages.KoiPage
{
    public class KoiCreateModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty]
        public KoiDto Koi { get; set; }
        public List<PondDto> Ponds { get; set; }

        public KoiCreateModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> OnGet()
        {
            Ponds = await _apiService.GetAsync<List<PondDto>>("api/pond");
            ViewData["PondId"] = new SelectList(Ponds, "Id", "PondName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _apiService.PostAsync<BusinessResult>("api/kois", Koi);
                if (result != null && result.Status == 1)
                {
                    return RedirectToPage("/KoiPage/KoiIndex");
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

            return Page();
        }
    }
}
