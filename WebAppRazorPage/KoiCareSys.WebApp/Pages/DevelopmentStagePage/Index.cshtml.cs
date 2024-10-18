using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.DevelopmentStagePage
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;
            DevelopmentStages = new List<DevelopmentStageDTO>(); // Initialize the list

        }

        public List<DevelopmentStageDTO> DevelopmentStages { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<List<DevelopmentStageDTO>>>("api/DevelopmentStage");
                DevelopmentStages = response.Data; // Access the data property

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync(string? keyword)
        {
            // call search api
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<List<DevelopmentStageDTO>>>("api/DevelopmentStage/search?keyword=" + keyword);
                DevelopmentStages = response?.Data ?? new List<DevelopmentStageDTO>(); // Fallback to empty list if response is null
                if (response.Status == 1)
                {
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                }
                return Page();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
                return Page();
            }
        }
    }
}
