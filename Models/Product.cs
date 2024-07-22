using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NorthwindOrdersAPI.Models
{
    public class Product
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }
        public int? CategoryID { get; set; }
        public Category? Category { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
