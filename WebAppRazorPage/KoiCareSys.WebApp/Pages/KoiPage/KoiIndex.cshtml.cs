using KoiCareSys.WebApp.ApiService.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.KoiPage
{
    public class KoiIndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public KoiIndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet()
        {
        }
    }
}
