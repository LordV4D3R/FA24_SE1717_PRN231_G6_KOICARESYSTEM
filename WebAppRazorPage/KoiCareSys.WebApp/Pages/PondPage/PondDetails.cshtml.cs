using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.PondPage
{
    public class PondDetailsModel : PageModel
    {
        private readonly IApiService _apiService;
        public PondDetailsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public PondDto Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            try
            {
                var response = await _apiService.GetAsync<PondDto>("api/Pond/" + id);
                if (response != null)
                {
                    Request = response;
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "");
                    return Page();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
                return Page();
            }
        }

    }
}
