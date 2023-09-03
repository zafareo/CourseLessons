using EmployeeMVC.Data;
using EmployeeMVC.Enities;
using EmployeeMVC.Models;
using EmployeeMVC.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeMVCContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRabbitMQProducer _rabitMQProducer;

        public EmployeeController(EmployeeMVCContext context, IWebHostEnvironment webHostEnvironment, IRabbitMQProducer rabbitMQProducer)
        {
                _context = context;
                _webHostEnvironment = webHostEnvironment;
                _rabitMQProducer = rabbitMQProducer;
        }
         
        public async Task<IActionResult> Index()
        {
            return _context.Employees != null ? View(await _context.Employees.ToListAsync())
                : NotFound("There are no employees in the company yet!");
        }

        public async Task<IActionResult> CreateEmployee()
        {
            return await Task.FromResult(View());
        }


       
        [HttpPost]
        [EnableRateLimiting("SlidingLimiter")]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeViewModel model)
        {
            if(ModelState.IsValid)
            {
                string RootPath = _webHostEnvironment.WebRootPath;
                string file = Guid.NewGuid() + model.Picture.FileName;
                string RootForFile = Path.Combine(RootPath + @"\img", file);

                using(var stream = new FileStream(RootForFile, FileMode.Create))
                {
                    await model.Picture.CopyToAsync(stream);
                }

                Employee? employee = new()
                {
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    Picture = "/img/" + file,
                    Profession = model.Profession
                };
                 _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                _rabitMQProducer.SendProductMessage(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EditEmployee(Guid? id)
        {
            if(id != null || _context.Employees != null)
            {
                Employee? employee = await _context.Employees.FindAsync(id);
                return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Guid id, [Bind("Id,Name,Birthdate,Picture,Profession")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               _context.Employees.Update(employee);
               await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _rabitMQProducer.SendProductMessage(employee);
            return View(employee);
        }

        public async Task<IActionResult> DeleteEmployee(Guid? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            Employee? employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _rabitMQProducer?.SendProductMessage(employee);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Employees == null)
            {
                return Problem("No employees in db context!");
            }
            Employee? employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }


}
