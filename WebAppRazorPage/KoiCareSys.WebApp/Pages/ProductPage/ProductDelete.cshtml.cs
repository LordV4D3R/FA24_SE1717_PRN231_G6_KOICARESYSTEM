using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiCareSys.WebApp.Pages.ProductPage
{
    public class ProductDeleteModel : PageModel
    {
        private readonly IApiService _apiService;

        public ProductDeleteModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public ProductDTO Product { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToPage("/ProductPage/ProductIndex");
            }

            try
            {
                Product = await _apiService.GetAsync<ProductDTO>($"api/products/{id}");

                if (Product == null)
                {
                    return RedirectToPage("/ProductPage/ProductIndex");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
                return RedirectToPage("/ProductPage/ProductIndex");
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            try
            {
                Product = await _apiService.GetAsync<ProductDTO>($"api/products/{id}");
                var result = await _apiService.DeleteAsync<PondDto>($"api/products/{Product.Id}");

                if (result == null)
                {
                    return RedirectToPage("/Products/ProductIndex");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the pond.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }


            return RedirectToPage("/ProductPage/Index"); ;
        }
    }
}
