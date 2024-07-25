using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Data.DTO
{
    public class ShipperDTO
    {
        public int ShipperID { get; set; }
        public string? ShipperName { get; set; }
        public string? Phone { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }


}
