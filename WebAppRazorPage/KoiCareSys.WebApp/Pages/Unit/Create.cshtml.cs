using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KoiCareSys.WebApp.Pages.Unit
{
    public class CreateModel : PageModel
    {
        private readonly IApiService _apiService;

        public CreateModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public UnitDto Unit { get; set; }

        public void OnGet()
        {
        }

        //public async Task<IActionResult> OnPostAsync([FromBody] UnitDto unit)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var response = await _apiService.PostAsync<UnitDto>("api/Unit", unit);
        //        if (response != null)
        //        {
        //            return new JsonResult(new { success = true });
        //        }
        //        else
        //        {
        //            return new JsonResult(new { success = false, error = "Unable to create unit." });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { success = false, error = ex.Message });
        //    }
        //}
    }
}
