using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.DevelopmentStagePage
{

    public class CreateModel : PageModel
    {
        private readonly IApiService _apiService;

        public CreateModel(IApiService apiService)
        {
            _apiService = apiService;
        }
        [BindProperty]
        public DevelopmentStageDTO Request { get; set; } = default!;

        public void OnGet()
        {
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var response = await _apiService.PostAsync<ApiResponse<DevelopmentStageDTO>>("api/DevelopmentStage", Request);
                if (response.Status == 1)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                    return Page();
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
