using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Models;
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
            _db = db;
        }
        public async Task<ActionResult> Index(int departmentId)
        {
            ViewBag.departmentId = departmentId;
            var employee = await _employeeRepository.GetEmployees(departmentId);
            return View(employee);
        }

        [HttpGet]
        public ActionResult InsertEmployee(int departmentId)
        {
            Employee emp = new Employee();
            emp.DepartmentId = departmentId;
            return View(emp);
        }
        [HttpPost]
        public ActionResult InsertEmployee(Employee insertEmployee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Employees.Add(insertEmployee);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Employee", new { departmentId = insertEmployee.DepartmentId });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while saving the entity changes: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
            }
            return View(insertEmployee);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return View("Error");
            }
            return View(employee);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(Employee updatedEmployee)
        {
            if (ModelState.IsValid)
            {
                var employee = _db.Employees.Find(updatedEmployee.EmployeeId);
                employee.EmployeeName = updatedEmployee.EmployeeName;
                _db.Employees.Update(employee);
                _db.SaveChanges();
                return RedirectToAction("Index", "Employee", new { departmentId = updatedEmployee.DepartmentId });
            }
            return View(updatedEmployee);
        }
        public ActionResult DeleteDepartment(int id)
        {
            var dept = _db.Departments.Find(id);
            if (dept == null)
            {
                return BadRequest();
            }
            _db.Departments.Remove(dept);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteEmployee(int id, int departmentId)
        {
            var emp = _db.Employees.Find(id);
            if (emp == null)
            {
                return BadRequest();
            }
            _db.Employees.Remove(emp);
            _db.SaveChanges();
            return RedirectToAction("Index",new{departmentId = departmentId });
        }
    }
}
   
