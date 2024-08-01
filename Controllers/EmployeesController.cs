using Microsoft.AspNetCore.Mvc;
using NorthwindOrdersAPI.Models;
using NorthwindOrdersAPI.Services;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeesController(EmployeeService employeeService) { this.employeeService = employeeService; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            return Ok(await employeeService.GetAllEmployeesAsync());
        }

    }

}
