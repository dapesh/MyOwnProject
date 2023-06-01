using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyOwnProject.Data;
using MyOwnProject.Models;
using MyOwnProject.Services;
using System.ComponentModel.Design;

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
        public async Task<ActionResult> Index(int companyid)
        {
           
            ViewBag.companyid =companyid;
            var department =await _departmentRepository.GetDepartments(companyid);
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
                //var oldDepartment = await _db.Departments.FirstOrDefaultAsync(x=>x.DepartmentId == updatedDepartment.DepartmentId);
                //oldDepartment.DepartmentName = updatedDepartment.DepartmentName;
                //oldDepartment.DepartmentAddress = updatedDepartment.DepartmentAddress;
                var department = _db.Departments.Find(updatedDepartment.DepartmentId);
                department.DepartmentName = updatedDepartment.DepartmentName;
                department.DepartmentAddress = updatedDepartment.DepartmentAddress;
                _db.Departments.Update(department);
                _db.SaveChanges();
                return RedirectToAction("Index", "Department",new { companyid = updatedDepartment.CompanyId});
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
        public ActionResult InsertDepartment(int companyid)
        {
            if (companyid == 0)
            {
                //var companyLst = _db.Companies.ToList();
                List<Company> companyList = _db.Companies.ToList();
                ViewBag.CompantList = companyList;
                return View();
            }
            else
            {
                Department dept = new Department();
                dept.CompanyId = companyid;
                return View(dept);
            }
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
                    return RedirectToAction("Index", "Department",new { companyid =insertDepartment.CompanyId});
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
