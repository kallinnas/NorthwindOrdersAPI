﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.Models;

namespace NorthwindOrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EmployeesController(AppDBContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            try
            {
                var employee = await _context.Employees
                .Select(e => new Employee
                {
                    EmployeeID = e.EmployeeID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    BirthDate = e.BirthDate,
                    Photo = e.Photo,
                    Notes = e.Notes,
                }).ToListAsync();

                return Ok(employee);
            }

            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return BadRequest($"An error occurred while retrieving orders: {ex.InnerException?.Message}");
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine($"{DateTime.UtcNow}: {ex.Message} {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving orders: {ex.Message}");
            }
        }

    }
}
