using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public ShippersController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipper>>> GetCustomers()
        {
            var shipper = await _context.Shippers
                .Select(c => new Shipper
                {
                    ShipperID = c.ShipperID,
                    ShipperName = c.ShipperName,
                    Phone = c.Phone
                }).ToListAsync();

            return Ok(shipper);
        }

    }
}
