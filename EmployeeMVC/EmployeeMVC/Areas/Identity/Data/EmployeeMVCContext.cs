using EmployeeMVC.Areas.Identity.Data;
using EmployeeMVC.Enities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Data;

public class EmployeeMVCContext : IdentityDbContext<User>
{
    public DbSet<Employee> Employees { get; set; }
    public EmployeeMVCContext(DbContextOptions<EmployeeMVCContext> options)
        : base(options)
    {
    }
}
