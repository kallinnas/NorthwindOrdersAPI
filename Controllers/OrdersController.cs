using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Data;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OrdersController(AppDBContext context)
        {
            _context = context;
        }
    }
}
