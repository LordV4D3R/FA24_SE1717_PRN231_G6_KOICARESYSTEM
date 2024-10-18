using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.Unit
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<UnitDto>? Units { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                //var response = await _apiService.GetAsync<BusinessResult<List<UnitDto>>>("api/Unit");
                //Units = response.Data;
                Units = new List<UnitDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
            }
        }
    }
}
