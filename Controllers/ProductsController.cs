using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetEmployee()
        {
            return Ok(await productService.GetProductsAsync());
        }

    }
}
