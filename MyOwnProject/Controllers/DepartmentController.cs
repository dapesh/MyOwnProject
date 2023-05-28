using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Models;
using MyOwnProject.Services;

namespace MyOwnProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _db;
        public DepartmentController(IDepartmentRepository departmentRepository, ApplicationDbContext db)
        {
            _departmentRepository = departmentRepository;
            _db = db;
        }
        public async Task<ActionResult> Index()
        {
            var department =await _departmentRepository.GetDepartments();
            return View(department);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateDepartment(int id)
        {
            var company = _db.Departments.Find(id);
            if (company == null)
            {
                return View("Error");
            }
            return View(company);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateDepartment(Department updatedDepartment)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Update(updatedDepartment);
                _db.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            return View(updatedDepartment);
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
        [HttpGet]
        public ActionResult InsertDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertDepartment(Department insertDepartment)
        {
            if (ModelState.IsValid)
            {
                //insertDepartment.CompanyId = 4;
                _db.Departments.Add(insertDepartment);
                _db.SaveChanges();
                return RedirectToAction("Index", "Department");
            }
            return View(insertDepartment);
        }
    }
}
