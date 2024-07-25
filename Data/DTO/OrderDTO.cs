using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Data.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public EmployeeDTO? Employee { get; set; }
        public CustomerDTO? Customer { get; set; }
        public ShipperDTO? Shipper { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotalPrice { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}
