using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void AddEmployee(T obj);
        public List<T> GetEmployees();
        public T GetEmployeeById(int id);
        public bool UpdateEmployee(T entity);
        public bool RemoveEmployee(int id);
    }
}
