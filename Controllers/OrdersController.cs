using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services.Interfaces;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet("Exists/{id}")]
        public async Task<ActionResult<bool>> IsOrderExists(int id)
        {
            return Ok(await orderService.IsOrderExistsAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersWithDetails()
        {
            return Ok(await orderService.GetOrdersWithDetailsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await orderService.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("CreateWithFile")]
        public async Task<ActionResult<bool>> PostOrder([FromForm] string newOrder, IFormFile file)
        {
            return Ok(await orderService.CreateOrderWithFileAsync(newOrder, file));
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateOrderAndDetails(int id, Order order)
        {
            return Ok(await orderService.UpdateOrderAndDetailsAsync(order));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await orderService.DeleteOrderAsync(id);

            if (!result)
            {
                return NotFound("Order not found.");
            }

            return Ok(true);
        }
    }
}
