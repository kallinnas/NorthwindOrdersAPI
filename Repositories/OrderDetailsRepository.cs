using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class OrderDetailsRepository
    {
        private readonly AppDBContext context;

        public OrderDetailsRepository(AppDBContext context) { this.context = context; }

        public async Task<int> SaveDBChanges()
        {
            return await context.SaveChangesAsync();
        }

        public void AddDetailToOrder(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
        }

        public async Task<bool> AddOrderDetails(List<OrderDetail> orders)
        {
            context.OrderDetails.AddRange(orders);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderDetails(List<OrderDetail> orders)
        {
            context.OrderDetails.RemoveRange(orders);
            await context.SaveChangesAsync();
            return true;
        }

    }
}