using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CustomersController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customer = await _context.Customers
                .Select(c => new Customer
                {
                    CustomerID = c.CustomerID,
                    CustomerName = c.CustomerName,
                    ContactName = c.ContactName,
                    Address = c.Address,
                    City = c.City,
                    PostalCode = c.PostalCode,
                    Country = c.Country
                }).ToListAsync();

                return Ok(customer);
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
