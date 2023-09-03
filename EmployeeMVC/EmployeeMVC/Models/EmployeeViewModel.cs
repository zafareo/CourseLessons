using EmployeeMVC.Enities;

namespace EmployeeMVC.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile Picture { get; set; }
        public Profession Profession { get; set; }
    }
}
