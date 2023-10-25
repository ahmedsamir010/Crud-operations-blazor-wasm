using Crud.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tables;

namespace Crud.API.Controllers
{

    public class EmployeeController : BaseController
    {
        private readonly EmpDbContext _dbContext;

        public EmployeeController(EmpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                var existingEmployee = await _dbContext.Employees.FindAsync(id);

                if (existingEmployee == null)
                {
                    return NotFound("Employee not found");
                }

                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.Email = employee.Email;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Gender = employee.Gender;


                await _dbContext.SaveChangesAsync();

                return Ok("Employee updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the employee: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return Ok("Employee Deleted successfully");
        }

        private bool EmployeeExists(int id)
        {
            return _dbContext.Employees.Any(e => e.Id == id);
        }
    }
}
