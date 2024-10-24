using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiCareSys.Data;
using KoiCareSys.Data.Models;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Serivice.Base;
using KoiCareSys.Data.DTO;
using KoiCareSys.Service.Service;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            var result = await _productService.GetAll();
            if (result != null && result.Status > 0 && result.Data != null)
            {
                return Ok(result.Data as IEnumerable<Product>);
            }
            else
            {
                return NotFound(result.Message ?? "No products found");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(Guid id)
        {
        

            try
            {
                var result = await _productService.GetById(id);
                if (result.Status > 0)
                {
                    return  Ok(result.Data);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutProduct([FromBody] ProductDTO request)
        {
            return await _productService.Update(request);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> PostProduct(ProductDTO product)
        {

            try
            {
                var result = await _productService.Create(product);
                if (result.Status > 0)
                {
                    return Ok(result.Data);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteProduct(Guid id)
        {
          return await _productService.DeleteById(id);  
        }

       
    }
}
