using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Varelagerstyring.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Varelagerstyring.Controllers
{
    [ApiController]
    [Route("api/varelagerstyring")]
    public class ProductsController : ControllerBase
    {
        private readonly IVarelagerstyringService varelagerstyringService;

        public ProductsController(IVarelagerstyringService varelagerstyringService)
        {
            this.varelagerstyringService = varelagerstyringService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var all = await varelagerstyringService.GetVarelagerstyringsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Varelagerstyring);
            }
            return NotFound(all.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await varelagerstyringService.GetVarelagerstyringAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Varelagerstyring);
            }
            return NotFound(result.ErrorMessage);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] Models.Varelagerstyring product)
        {
            var newproduct = await varelagerstyringService.PostVarelagerstyringAsync(product);
            if (newproduct.IsSuccess)
            {
                return Ok(newproduct.Varelagerstyring);
            }
            return NotFound(newproduct.ErrorMessage);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var product = await varelagerstyringService.DeleteVarelagerstyringAsync(id);
            if (product.IsSuccess)
            {
                return Ok();
            }
            return NotFound(product.ErrorMessage);
        }
    }
}
