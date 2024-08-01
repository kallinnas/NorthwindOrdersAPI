using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class OrderRepository
    {
        private readonly AppDBContext context;

        public OrderRepository(AppDBContext context) { this.context = context; }

        public IQueryable<Order> GetAllOrdersAsync()
        {
            return context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Include(o => o.Shipper)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product);
        }

        public async Task<bool> IsOrderExistsAsync(int id)
        {
            return await context.Orders.AnyAsync(o => o.OrderID == id);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await GetAllOrdersAsync().FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderAndDetailsAsync(Order order)
        {
            Order? existingOrder = await GetOrderAndOrderDetailsAsync(order.OrderID);

            if (existingOrder == null)
            {
                throw new InvalidOperationException($"Order with ID {order.OrderID} not found.");
            }

            existingOrder.UpdateOrderValues(order);

            var existingDetails = existingOrder.OrderDetails.ToList();
            var newDetails = order.OrderDetails.ToList();

            // Remove old orderDetails from db
            foreach (var detail in existingDetails)
            {
                if (!newDetails.Any(d => d.OrderDetailID == detail.OrderDetailID))
                {
                    context.OrderDetails.Remove(detail);
                }
            }

            foreach (var detail in newDetails)
            {
                var existingDetail = existingDetails.FirstOrDefault(d => d.OrderDetailID == detail.OrderDetailID);

                // Add brand new orderDetails
                if (existingDetail == null)
                {
                    detail.OrderID = order.OrderID;
                    context.OrderDetails.Add(detail);
                }
                // Update old orderDetails
                else
                {
                    existingDetail.ProductID = detail.ProductID;
                    existingDetail.Quantity = detail.Quantity;
                }
            }

            // Final save context changes
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UploadOrderDocumentAsync(OrderDocument document)
        {
            context.OrderDocuments.Add(document);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetOrderAndOrderDetailsAsync(int id)
        {
            return await context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public void RemoveOrder(Order order)
        {
            context.OrderDetails.RemoveRange(order.OrderDetails);
            context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
