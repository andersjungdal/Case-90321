using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product_Service.Interfaces;
using Product_Service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_Service.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var all = await productsProvider.GetProductsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Products);
            }
            return NotFound(all.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Product);
            }
            return NotFound(result.ErrorMessage);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Product product)
        {
            var newproduct = await productsProvider.PostProductAsync(product);
            if (newproduct.IsSuccess)
            {
                return Ok(newproduct.Product);
            }
            return NotFound(newproduct.ErrorMessage);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await productsProvider.DeleteProductAsync(id);
            if (product.IsSuccess)
            {
                return Ok();
            }
            return NotFound(product.ErrorMessage);
        }
    }
}
