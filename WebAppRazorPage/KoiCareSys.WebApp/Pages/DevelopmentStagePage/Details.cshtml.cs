using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.DevelopmentStagePage
{
    public class DetailsModel : PageModel
    {
        private readonly IApiService _apiService;
        public DetailsModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public DevelopmentStageDTO Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<DevelopmentStageDTO>>("api/DevelopmentStage/" + id);
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
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
                return Page();
            }
        }

    }
}
