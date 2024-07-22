namespace NorthwindOrdersAPI.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Category() { }
    }
}
