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
            KoiRecords = new List<KoiRecordDTO>();
        }

        public List<KoiRecordDTO> KoiRecords { get; set; }


        public async Task OnGetAsync()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<List<KoiRecordDTO>>>("api/KoiRecord");
                KoiRecords = response.Data; // Access the data property

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching koi record: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostAsync(string? keyword)
        {
            // call search api
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<List<KoiRecordDTO>>>("api/KoiRecord/search?keyword=" + keyword);
                KoiRecords = response?.Data ?? new List<KoiRecordDTO>(); // Fallback to empty list if response is null
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
                Console.WriteLine($"Error fetching koi records: {ex.Message}");
                return Page();
            }
        }
    }
}
