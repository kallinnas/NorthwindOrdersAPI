using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Repositories
{
    public class EmployeeRepository
    {
        private readonly AppDBContext context;

        public EmployeeRepository(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await context.Employees.ToListAsync();
        }
    }
}
