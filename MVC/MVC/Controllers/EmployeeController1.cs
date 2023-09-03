using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.ViewModel;
using System.Security.Cryptography.X509Certificates;

namespace MVC.Controllers
{
    public class EmployeeController1 : Controller
    {
        private readonly List<Employee> _employees;

        public EmployeeController1(List<Employee> employees)
        {
            _employees = employees;
        }
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if(employee != null)
            {
                 _employees.Add(employee);
                 return View("AddEmployeeView");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = _employees.FirstOrDefault(x => x.Id == id);
            return View("GetEmployeeByIdView",employee);
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            EmployeeView model = new()
            {
                employees = _employees
            };
            return View("GetAllEmployeesView",model);
        }

        [HttpDelete]
        public IActionResult DeleteEmployee(int id) 
        {
            _employees.Remove(_employees.FirstOrDefault(x=>x.Id == id));
            return RedirectToAction("GetAllEmployees");
        }
    }
}
