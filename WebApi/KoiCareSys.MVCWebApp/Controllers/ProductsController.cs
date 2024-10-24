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

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly ApplicationDbContext _context;

        //public ProductsController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

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
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Const.APIEndPoint + $"Products/{id}"))
                {
                    Console.WriteLine(Const.APIEndPoint + $"Products/{id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<BusinessResult>(content);

                        if (result != null && result.Data != null)
                        {
                            var data = JsonConvert.DeserializeObject<Product>(result.Data.ToString());
                            return View(data);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Price,SalePrice,TotalSold,ImgUrl,Description,Status,isDeleted")] ProductDTO product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                // Serialize the product to JSON
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request
                using (var response = await httpClient.PutAsync(Const.APIEndPoint + "Products", content))
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

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ProductExists(Guid id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}
