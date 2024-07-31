using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services.Interfaces;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService, AppDBContext context)
        {
            this.orderService = orderService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersWithDetails()
        {
            var orderDtos = await orderService.GetOrdersWithDetailsAsync();
            return Ok(orderDtos);
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
            try
            {
                var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id);

                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                _context.OrderDetails.RemoveRange(order.OrderDetails);
                _context.Orders.Remove(order);

                await _context.SaveChangesAsync();
                return Ok(true);
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
