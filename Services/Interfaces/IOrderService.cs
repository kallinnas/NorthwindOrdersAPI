using NorthwindOrdersAPI.Data.DTO;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CreateOrderWithFileAsync(string newOrder, IFormFile file);
        Task<bool> UpdateOrderAndDetailsAsync(Order order);
        Task<IEnumerable<OrderDTO>> GetOrdersWithDetailsAsync();
        Task<OrderDTO> GetOrderAsync(int id);
        Task<bool> IsOrderExistsAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
    }
}
