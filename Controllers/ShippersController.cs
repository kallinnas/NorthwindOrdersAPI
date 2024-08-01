using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly ShipperService shipperService;

        public ShippersController(ShipperService shipperService)
        {
            this.shipperService = shipperService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipper>>> GetCustomers()
        {
                return Ok(await shipperService.GetShippersAsync());
        }

    }
}
