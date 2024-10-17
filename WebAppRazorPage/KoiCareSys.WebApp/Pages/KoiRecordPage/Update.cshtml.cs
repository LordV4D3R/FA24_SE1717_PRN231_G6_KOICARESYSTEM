using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.KoiRecordPage
{
    public class UpdateModel : PageModel
    {
        private readonly IApiService _apiService;
        public UpdateModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public KoiRecordDTO Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<KoiRecordDTO>>("api/KoiRecord/" + id);
                if (response.Status == 1)
                {
                    Request = response.Data;
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                    return Page();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching koi record: {ex.Message}");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var response = await _apiService.PutAsync<ApiResponse<KoiRecordUpdateDTO>>("api/KoiRecord", Request);
                if (response.Status == 1)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                    return Page();
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
