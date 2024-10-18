using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.Measurement
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<MeasurementDto>? Measurements { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                var response = await _apiService.GetAsync<BusinessResult<List<MeasurementDto>>>("api/Measurement");
                Measurements = response.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
            }
        }
    }
}
