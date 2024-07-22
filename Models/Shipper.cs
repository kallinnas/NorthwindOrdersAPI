using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindOrdersAPI.Models
{
    public class Shipper
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ShipperID { get; set; }
        public string? ShipperName { get; set; }
        public string? Phone { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
