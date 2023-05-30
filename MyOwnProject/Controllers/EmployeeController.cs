using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Services;

namespace MyOwnProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _db;
        public EmployeeController(IEmployeeRepository employeeRepository, ApplicationDbContext db)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<ActionResult> Index()
        {
            var employee = await _employeeRepository.GetEmployees();
            return View(employee);
        }
    }
}
