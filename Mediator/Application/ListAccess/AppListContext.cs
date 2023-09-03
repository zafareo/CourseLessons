using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ListAccess
{
    public class AppListContext
    {
        public static List<Employee> Employees { get; set; }
    }
}
