using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindOrdersAPI.Models
{
    public class Customer
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
