using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Base;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KoiCareSys.WebApp.Pages.KoiPage
{
    public class KoiIndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public KoiIndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<KoiDto> Kois { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/kois");
                if (result.Data is not null)
                {
                    Kois = JsonConvert.DeserializeObject<List<KoiDto>>(result.Data.ToString());
                }
                else
                {
                    Kois = new List<KoiDto>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }
        }
    }
}
