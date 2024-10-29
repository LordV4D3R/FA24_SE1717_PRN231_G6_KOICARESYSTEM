using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiCareSys.Data;
using KoiCareSys.Data.Models;
using KoiCareSys.Common;
using KoiCareSys.Serivice.Base;
using Newtonsoft.Json;
using KoiCareSys.Data.DTO;
using KoiCareSys.MVCWebApp.ApiService.Interface;
using Azure;
using KoiCareSys.MVCWebApp.Models;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private IApiService _apiService;
       

        public ProductsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string? name, string? description, int? minValue, int? maxValue)
        {

            var kois = new List<Product>();
            try
            {
                var result = await _apiService.GetAsync<BusinessResult>("api/products");
                if (result != null && result.Status == 1)
                {
                    kois = JsonConvert.DeserializeObject<List<Product>>(result.Data.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching ponds: {ex.Message}");
            }

            var filteredKois = kois
       .Where(k =>
           (string.IsNullOrEmpty(name) || k.Name.ToLower().Contains(name.ToLower())) &&
           (string.IsNullOrEmpty(description) || k.Description.ToLower().Contains(description.ToLower())) &&
           (!minValue.HasValue || k.Price >= minValue) &&
           (!maxValue.HasValue || k.Price <= maxValue)
       );
            return View("Index", filteredKois);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + "Products"))
                {
                    Console.WriteLine(Const.APIEndPoint + "Products");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<List<Product>>(result.Data.ToString());
                            return View(data);
                        }
                    }
                }
            }
            return View(new List<Product>());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Products/{id}"))
                {
                    Console.WriteLine(Const.APIEndPoint + "Products");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Product>(content);

                        if (result != null )
                        {
                            //var data = JsonConvert.DeserializeObject<Product>(result);
                            return View(result);
                        }
                    }
                }
            }
            return View(new Product());
        }

        //// GET: Products/Create
        public IActionResult Create()
        {

            return View();
        }

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,SalePrice,TotalSold,ImgUrl,Description,Status")] ProductDTO product)
        {
          
                using (var httpClient = new HttpClient())
                {
                    // Serialize the product to JSON
                    var json = JsonConvert.SerializeObject(product);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    // Send the POST request
                    using (var response = await httpClient.PostAsync(Const.APIEndPoint + "Products", content))
                    {
                        Console.WriteLine(Const.APIEndPoint + "Products");

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);

                            if (result != null && result.Data != null)
                            {
                                var data = JsonConvert.DeserializeObject<List<Product>>(result.Data.ToString());
                                return View(data);
                            }
                        }
                    }
                }
                return View(product);
            }



        //// GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Products/{id}"))
                {
                    Console.WriteLine(Const.APIEndPoint + "Products");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Product>(content);

                        if (result != null)
                        {
                            //var data = JsonConvert.DeserializeObject<Product>(result);
                            return View(result);
                        }
                    }
                }
            }
            return View(new Product());
        }

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Price,SalePrice,TotalSold,ImgUrl,Description,Status,isDeleted")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                SalePrice = product.SalePrice,
                TotalSold = product.TotalSold,
                ImgUrl = product.ImgUrl,
                Description = product.Description,
                Status = product.Status,
                isDeleted = product.isDeleted
            };



            using (var httpClient = new HttpClient())
            {
                // Serialize the product to JSON
                var json = JsonConvert.SerializeObject(productDTO);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request
                using (var response = await httpClient.PutAsync(Const.APIEndPoint + $"Products/{id}", content))
                {
                    // Log URL để kiểm tra
                    Console.WriteLine(Const.APIEndPoint + $"Products/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(responseContent);

                        if (result != null )
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    else
                    {
                        // Log lỗi nếu không thành công
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error: " + errorContent);
                    }
                }
            }
            return View(product);
            
        

        }

        //// GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Products/{id}"))
                {
                    Console.WriteLine(Const.APIEndPoint + "Products");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Product>(content);

                        if (result != null)
                        {
                            //var data = JsonConvert.DeserializeObject<Product>(result);
                            return View(result);
                        }
                    }
                }
            }
            return View(new Product());


           
        }

        //// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _apiService.DeleteAsync<BusinessResult>($"api/Products/{id}");
            if (result != null && result.Status == 1)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting Koi.");
            return View();
        }

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}
