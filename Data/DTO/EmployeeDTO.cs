using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Data
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[]? Photo { get; set; }
        public string? Notes { get; set; }

    }
}
