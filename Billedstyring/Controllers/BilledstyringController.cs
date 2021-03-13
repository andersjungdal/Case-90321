using System.Threading.Tasks;
using Billedstyring.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billedstyring.Controllers
{
    [ApiController]
    [Route("api/billedstyring")]
    public class ProductsController : ControllerBase
    {
        private readonly IBilledstyringService billedstyringService;

        public ProductsController(IBilledstyringService billedstyringService)
        {
            this.billedstyringService = billedstyringService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBilledstyringsAsync()
        {
            var all = await billedstyringService.GetBilledstyringsAsync();
            if (all.IsSuccess)
            {
                return Ok(all.Billedstyrings);
            }
            return NotFound(all.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBilledstyringAsync(int id)
        {
            var result = await billedstyringService.GetBilledstyringAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Billedstyring);
            }
            return NotFound(result.ErrorMessage);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBilledstyringAsync([FromBody] Models.Billedstyring Billedstyring)
        {
            var newproduct = await billedstyringService.PostBilledstyringAsync(Billedstyring);
            if (newproduct.IsSuccess)
            {
                return Ok(newproduct.Billedstyring);
            }
            return NotFound(newproduct.ErrorMessage);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilledstyringAsync(int id)
        {
            var product = await billedstyringService.DeleteBilledstyringAsync(id);
            if (product.IsSuccess)
            {
                return Ok();
            }
            return NotFound(product.ErrorMessage);
        }
    }
}
