using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Repositories;

namespace NorthwindOrdersAPI.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await employeeRepository.GetEmployeesAsync();
        }
    }
}
