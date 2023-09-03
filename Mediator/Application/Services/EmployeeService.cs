using Application.Common.Interfaces;
using Application.ListAccess;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        public void AddEmployee(Employee obj)
        {
            AppListContext.Employees.Add(obj);
        }

        public Employee GetEmployeeById(int id)
        {
            return AppListContext.Employees.FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return AppListContext.Employees;
        }

        public bool RemoveEmployee(int id)
        {
            Employee? employee = AppListContext.Employees.FirstOrDefault(emp => emp.Id == id);
            if (employee != null)
            {
                AppListContext.Employees.Remove(employee);
                return true;
            }
            return false;            
        }

        public bool UpdateEmployee(Employee entity)
        {
            Employee? employee = AppListContext.Employees.FirstOrDefault(emp => emp.Id == entity.Id);
            if (employee != null)
            {
                employee.Name = entity.Name;
                employee.Email = entity.Email;
                return true;
            }
            return false;
        }
    }
}
