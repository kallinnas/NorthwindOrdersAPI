using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class TempOrderRepository
    {
        private readonly AppDBContext context;

        public TempOrderRepository(AppDBContext context) { this.context = context; }

        public async Task<Guid> SaveTempOrderAsync(TempOrder tempOrder)
        {
            context.TempOrders.Add(tempOrder);
            await context.SaveChangesAsync();
            return tempOrder.Id;
        }

        public async Task<bool> UpdateTempOrderAsync(Guid id, TempOrder tempOrder)
        {
            TempOrder? existingTempOrder = await GetTempOrderAsync(id);

            if (existingTempOrder == null)
            {
                throw new InvalidOperationException($"Order with ID {id} not found.");
            }

            existingTempOrder.UpdateTempOrderValues(tempOrder);

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<TempOrder?> GetTempOrderAsync(Guid id)
        {
            return await context.TempOrders.FindAsync(id);
        }

        public async Task<bool> DeleteTempOrderAsync(Guid id)
        {
            var tempOrder = await context.TempOrders.FindAsync(id);
            if (tempOrder == null) return false;

            context.TempOrders.Remove(tempOrder);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
