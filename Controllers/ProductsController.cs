using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ProductsController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetEmployee()
        {
            try
            {
                var product = await _context.Products
                .Select(p => new Product
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    SupplierID = p.SupplierID,
                    CategoryID = p.CategoryID,
                    Unit = p.Unit,
                    Price = p.Price,
                }).ToListAsync();

                return Ok(product);
            }

            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return BadRequest($"An error occurred while retrieving orders: {ex.InnerException?.Message}");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving orders: {ex.Message}");
            }
        }

    }
}
