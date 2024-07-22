namespace NorthwindOrdersAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[]? Photo { get; set; }
        public string? Notes { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
