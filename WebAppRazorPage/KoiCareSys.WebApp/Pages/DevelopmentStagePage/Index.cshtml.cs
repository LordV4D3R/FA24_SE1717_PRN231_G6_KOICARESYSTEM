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
        }

        public List<DevelopmentStageDTO> DevelopmentStages { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var response = await _apiService.MyGetAsync<List<DevelopmentStageDTO>>("api/DevelopmentStage");
                DevelopmentStages = response.Data; // Access the data property

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
            }
        }
    }
}
