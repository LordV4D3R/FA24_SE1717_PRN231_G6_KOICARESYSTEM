using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.PondPage
{
    public class PondIndexModel : PageModel
    {
        private readonly IApiService _apiService;

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public PondIndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<PondDto> Ponds { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                var endpoint = string.IsNullOrEmpty(SearchQuery)
                        ? "api/pond"
                        : $"api/pond?search={Uri.EscapeDataString(SearchQuery)}";
                Ponds = await _apiService.GetAsync<List<PondDto>>(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }
        }

    }
}
