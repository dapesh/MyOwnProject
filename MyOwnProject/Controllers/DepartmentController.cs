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
    }
}
