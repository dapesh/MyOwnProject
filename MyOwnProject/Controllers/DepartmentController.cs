using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var department = _db.Departments.Find(id);
            if (department == null)
            {
                return View("Error");
            }
            return View(department);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateDepartment(Department updatedDepartment)
        {
            if (ModelState.IsValid)
            {
                var oldDepartment = await _db.Departments.FirstOrDefaultAsync(x=>x.DepartmentId == updatedDepartment.DepartmentId);
                oldDepartment.DepartmentName = updatedDepartment.DepartmentName;
                oldDepartment.DepartmentAddress = updatedDepartment.DepartmentAddress;
                _db.Departments.Update(oldDepartment);
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
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Departments.Add(insertDepartment);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Department");
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
            return View(insertDepartment);
        }
    }
}
