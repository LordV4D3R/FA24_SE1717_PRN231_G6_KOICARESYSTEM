using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KoiCareSys.WebApp.Pages.Unit
{
    public class UpdateModel : PageModel
    {
        private readonly IApiService _apiService;

        public UpdateModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UnitDto Unit { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var response = await _apiService.GetAsync<BusinessResult<UnitDto>>($"api/Unit/{id}");
            if (response.Data == null)
            {
                return NotFound();
            }

            Unit = response.Data;
            return Page();
        }
    }
}
