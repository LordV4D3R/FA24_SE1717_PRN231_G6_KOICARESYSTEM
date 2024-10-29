using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;

namespace WebApplication1.Pages.Shared
{
    public class IndexModel : PageModel


    {
        private readonly IApiService _apiService;


        public IndexModel(IApiService apiService)
        {
            _apiService = apiService;   
        }

        public IList<ProductDTO> Product { get; set; } = new List<ProductDTO>();

        public async Task OnGetAsync()
        {
            try
            {
                //var response = await _apiService.GetAsync<HttpResponseMessage>("api/products");
                //var content = await response.Content.ReadAsStringAsync();
                //Console.WriteLine("API Response: " + content);
                Product = await _apiService.GetAsync<List<ProductDTO>>("api/Products") ;
                if (  Product == null)
                { Console.WriteLine(":"); }
                Console.WriteLine($"Product count: {Product.Count}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }
        }
    }
}
