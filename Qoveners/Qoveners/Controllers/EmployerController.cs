using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qoveners.Entites;

namespace Qoveners.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IList<Employee> _employees;
        public EmployerController(IList<Employee> employees)
        {
            _employees = employees;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is invalid.");
            }
            _employees.Add(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            var existingEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employeeToDelete = _employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
