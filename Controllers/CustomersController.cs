using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public CustomersController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customer = await _context.Customers
                .Select(c => new CustomerDTO
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

    }
}
