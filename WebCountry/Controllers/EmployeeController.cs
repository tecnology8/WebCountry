using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCountry.Data;
using WebCountry.Models;
using WebCountry.ViewModel;

namespace WebCountry.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HumanResourceContext _context;
        public EmployeeController(HumanResourceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await _context.Employee.ToListAsync();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeViewModel model)
        {
            var employee = new Employee()
            {
                Name = model.Name,
                Email = model.Email,
                Salary = model.Salary,
                DateOfBirth = model.DateOfBirth,
                Department = model.Department
            };

            await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewEmployee = new UpdateEmployeeViewModel
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };

                return await Task.Run(() => View("View", viewEmployee));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await _context.Employee.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
             var employee = await _context.Employee.FindAsync(model.Id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
           
        }

    }
}