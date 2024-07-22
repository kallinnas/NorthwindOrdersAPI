using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindOrdersAPI.Models
{
    public class Order
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee? Employee { get; set; }
        public DateTime OrderDate { get; set; }
        public int? ShipperID { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Shipper? Shipper { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
