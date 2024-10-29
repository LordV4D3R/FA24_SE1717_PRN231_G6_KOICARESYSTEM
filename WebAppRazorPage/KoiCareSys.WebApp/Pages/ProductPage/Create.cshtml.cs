using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;

namespace WebApplication1.Pages.NewFolder
{
    public class CreateModel : PageModel
    {
        private readonly IApiService _apiService;

        public CreateModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ProductDTO Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var result = await _apiService.PostAsync<PondDto>("api/products", Product);

                if (result != null)
                {
                    return RedirectToPage("/ProductPage/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the pond.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred.");
            }

            return Page();

        }
    }
}
