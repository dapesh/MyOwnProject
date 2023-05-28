using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Models;
using MyOwnProject.Services;
using System.Diagnostics;

namespace MyOwnProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ApplicationDbContext _db;
        public HomeController(ICompanyRepository companyRepository, ApplicationDbContext db)
        {
            _companyRepository = companyRepository;
            _db=db;
        }
        public async Task<ActionResult> Index()
        {
            var company = await _companyRepository.GetCompanies();
            //Console.WriteLine(company);
            return View(company);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateCompany(int id)
        {
            var company = _db.Companies.Find(id);
            if(company == null)
            {
                return View("Error");
            }
            return View(company);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCompany(Company updatedCompany)
        {

            if (ModelState.IsValid)
            {
                _db.Companies.Update(updatedCompany);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(updatedCompany);
        }
        public ActionResult DeleteCompany(int id)
        {
            var company = _db.Companies.Find(id);
            if(company == null)
            {
                return BadRequest();
            }
            _db.Companies.Remove(company);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult InsertCompany()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertCompany(Company insertCompany)
        {
            if (ModelState.IsValid)
            {
                _db.Companies.Add(insertCompany);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(insertCompany);
        }
    }
}