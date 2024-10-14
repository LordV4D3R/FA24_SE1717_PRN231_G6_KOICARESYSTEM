using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.KoiRecordPage
{
    public class KoiRecordIndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public KoiRecordIndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<KoiRecordDTO> KoiRecords { get; set; }


        public async Task OnGetAsync()
        {
            try
            {
                var response = await _apiService.MyGetAsync<List<KoiRecordDTO>>("api/KoiRecord");
                KoiRecords = response.Data; // Access the data property

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching development stages: {ex.Message}");
            }
        }
    }
}
