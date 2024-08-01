using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;

namespace NorthwindOrdersAPI.Services
{
    public class ShipperService
    {
        private readonly ShipperRepository shipperRepository;

        public ShipperService(ShipperRepository shipperRepository) { this.shipperRepository = shipperRepository; }

        public async Task<IEnumerable<Shipper>> GetShippersAsync()
        {
            return await shipperRepository.GetAllShippersAsync();
        }
    }
}
