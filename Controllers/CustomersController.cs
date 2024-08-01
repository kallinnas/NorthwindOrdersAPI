using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService customerService;

        public CustomersController(CustomerService customerService) { this.customerService = customerService; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return Ok(await customerService.GetCustomersAsync());
        }

    }
}
