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
    public class EditModel : PageModel
    {
        private readonly IApiService _apiService;


        public EditModel(IApiService context)
        {
            _apiService = context;
        }

        [BindProperty]
        public ProductDTO Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            if (id == Guid.Empty)
            {
                return RedirectToPage("/ProductPage/Index");
            }

            try
            {
               Product = await _apiService.GetAsync<ProductDTO>($"api/products/{id}");

                if (Product == null)
                {
                    return RedirectToPage("/ProductPage/Index");
                }
            }
            catch
            {
                return RedirectToPage("/ProductPage/Index");
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           
            try
            {
                //Pond = await _apiService.GetAsync<PondDto>($"api/pond/{Pond.Id}");
                var result = await _apiService.PutAsync<ProductDTO>("api/products", Product);

                if (result != null)
                {
                    return RedirectToPage("/ProductPage/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            return Page();
        }

        
      
    }
}
