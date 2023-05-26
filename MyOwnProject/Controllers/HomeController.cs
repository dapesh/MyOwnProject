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
        public async Task<ActionResult> UpdateCompany(int id)
        {
            var company = _db.Companies.Find(id);
            if(company == null)
            {
                return View("Error");
            }
            return Ok();
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
    }
}