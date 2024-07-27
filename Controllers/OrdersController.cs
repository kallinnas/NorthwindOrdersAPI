using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDBContext _context;

        public OrdersController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orderDtos = await _context.Orders.Select(o => new OrderDTO
                {
                    OrderID = o.OrderID,
                    EmployeeID = o.EmployeeID,
                    EmployeeFirstName = o.Employee.FirstName,
                    EmployeeLastName = o.Employee.LastName,
                    EmployeeFullName = o.Employee.FirstName + " " + o.Employee.LastName,
                    CustomerID = o.CustomerID,
                    CustomerName = o.Customer.CustomerName,
                    ShipperID = o.ShipperID,
                    ShipperName = o.Shipper.ShipperName,
                    OrderDate = o.OrderDate,
                    OrderTotalPrice = (double)o.OrderDetails.Sum(od => od.Quantity * od.Product.Price)
                }).ToListAsync();

                return Ok(orderDtos);
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

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            try
            {
                var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.OrderID == id)
                .Select(o => new OrderDTO
                {
                    OrderID = o.OrderID,
                    EmployeeID = o.EmployeeID,
                    EmployeeFirstName = o.Employee.FirstName,
                    EmployeeLastName = o.Employee.LastName,
                    EmployeeFullName = o.Employee.FirstName + " " + o.Employee.LastName,
                    CustomerID = o.Customer.CustomerID,
                    CustomerName = o.Customer.CustomerName,
                    CustomerContactName = o.Customer.ContactName,
                    ShipperID = o.Shipper.ShipperID,
                    ShipperName = o.Shipper.ShipperName,
                    OrderDate = o.OrderDate,
                    OrderTotalPrice = (double)o.OrderDetails.Sum(od => od.Quantity * od.Product.Price),
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDTO
                    {
                        OrderDetailID = od.OrderDetailID,
                        OrderID = od.OrderID,
                        ProductID = od.ProductID,
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity,
                        UnitPrice = od.Product.Price
                    }).ToList()
                }).FirstOrDefaultAsync();

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
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

        [HttpPost("Create")]
        public async Task<ActionResult<OrderDTO>> PostOrder(Order order)
        {
            if (order == null || order.OrderDetails == null || order.OrderDetails.Count == 0)
            {
                return BadRequest("Order and order details are required.");
            }

            try
            {
                _context.Orders.Add(order);

                foreach (var detail in order.OrderDetails)
                {
                    _context.OrderDetails.Add(detail);
                }

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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            try
            {
                var existingOrder = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .FirstOrDefaultAsync(o => o.OrderID == id);

                if (existingOrder == null)
                {
                    return NotFound("Order not found.");
                }

                existingOrder.CustomerID = order.CustomerID;
                existingOrder.EmployeeID = order.EmployeeID;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.ShipperID = order.ShipperID;

                var existingDetails = existingOrder.OrderDetails.ToList();
                var newDetails = order.OrderDetails.ToList();

                foreach (var detail in existingDetails)
                {
                    if (!newDetails.Any(d => d.OrderDetailID == detail.OrderDetailID))
                    {
                        _context.OrderDetails.Remove(detail);
                    }
                }

                foreach (var detail in newDetails)
                {
                    var existingDetail = existingDetails.FirstOrDefault(d => d.OrderDetailID == detail.OrderDetailID);
                    if (existingDetail == null)
                    {
                        detail.OrderID = id;
                        _context.OrderDetails.Add(detail);
                    }
                    else
                    {
                        existingDetail.ProductID = detail.ProductID;
                        existingDetail.Quantity = detail.Quantity;
                    }
                }

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
