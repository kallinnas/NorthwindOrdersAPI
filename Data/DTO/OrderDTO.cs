using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Data.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public Employee? Employee { get; set; }
        public int EmployeeID { get; set; }
        public string? EmployeeLastName { get; set; }
        public string? EmployeeFirstName { get; set; }
        public string? EmployeeFullName { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerContactName { get; set; }
        public Shipper? Shipper { get; set; }
        public int? ShipperID { get; set; }
        public string? ShipperName { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotalPrice { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
