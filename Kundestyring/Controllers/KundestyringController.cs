using System.Threading.Tasks;
using Kundestyring.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kundestyring.Controllers
{
    [ApiController]
    [Route("api/kundestyring")]
    public class ProductsController : ControllerBase
    {
        private readonly IKundestyringService kundestyringService;

        public ProductsController(IKundestyringService kundestyringService)
        {
            this.kundestyringService = kundestyringService;
        }

        [HttpGet]
        public async Task<IActionResult> GetKundestyringsAsync()
        {
            var all = await kundestyringService.GetKundestyringsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Kundestyrings);
            }
            return NotFound(all.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKundestyringAsync(int id)
        {
            var result = await kundestyringService.GetKundestyringAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Kundestyring);
            }
            return NotFound(result.ErrorMessage);
        }
        [HttpPost]
        public async Task<IActionResult> CreateKundestyringAsync([FromBody] Models.Kundestyring Kundestyring)
        {
            var newproduct = await kundestyringService.PostKundestyringAsync(Kundestyring);
            if (newproduct.IsSuccess)
            {
                return Ok(newproduct.Kundestyring);
            }
            return NotFound(newproduct.ErrorMessage);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKundestyringAsync(int id)
        {
            var product = await kundestyringService.DeleteKundestyringAsync(id);
            if (product.IsSuccess)
            {
                return Ok();
            }
            return NotFound(product.ErrorMessage);
        }
    }
}
