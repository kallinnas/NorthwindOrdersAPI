using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class ShipperRepository
    {
        private readonly AppDBContext context;

        public ShipperRepository(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Shipper>> GetAllShippersAsync()
        {
            return await context.Shippers.ToListAsync();
            //return await context.Shippers
            //    .Select(s => new Shipper
            //    {
            //        ShipperID = s.ShipperID,
            //        ShipperName = s.ShipperName,
            //        Phone = s.Phone
            //    }).ToListAsync();
        }
    }
}
