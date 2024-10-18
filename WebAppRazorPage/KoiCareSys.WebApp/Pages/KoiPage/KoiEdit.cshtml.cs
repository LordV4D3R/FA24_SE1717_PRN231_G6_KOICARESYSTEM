using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Base;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KoiCareSys.WebApp.Pages.KoiPage
{
    public class KoiEditModel : PageModel
    {
        private readonly IApiService _apiService;

        public KoiEditModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public KoiDto Koi { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("/KoiPage/KoiIndex");
            }

            try
            {
                var result = await _apiService.GetAsync<BusinessResult>($"api/kois/{id}");
                if (result.Data == null)
                {
                    return RedirectToPage("/KoiPage/KoiIndex");
                }

                Koi = JsonConvert.DeserializeObject<KoiDto>(result.Data.ToString());
                if (Koi == null)
                {
                    return RedirectToPage("/KoiPage/KoiIndex");
                }
            }
            catch
            {
                return RedirectToPage("/KoiPage/KoiIndex");
            }

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
                var result = await _apiService.PutAsync<BusinessResult>($"api/kois/{Koi.Id}", Koi);
                if (result != null && result.Status == 1)
                {
                    return RedirectToPage("/KoiPage/KoiIndex");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the Koi.");
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
