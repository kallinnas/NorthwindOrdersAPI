using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class OrderDetailsRepository
    {
        private readonly AppDBContext _context;

        public OrderDetailsRepository(AppDBContext context) { _context = context; }

        public async Task<int> SaveDBChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void AddDetailToOrder(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
        }

        public async Task<bool> AddOrderDetails(List<OrderDetail> orders)
        {
            _context.OrderDetails.AddRange(orders);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveOrderDetails(List<OrderDetail> orders)
        {
            _context.OrderDetails.RemoveRange(orders);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task<bool> UpdateOrderDetails(int id)
        //{
        //    return
        //}

    }
}