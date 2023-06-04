using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Models;
using MyOwnProject.Services;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;

namespace MyOwnProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ICompanyRepository companyRepository, ApplicationDbContext db,IWebHostEnvironment webHostEnvironment)
        {
            _companyRepository = companyRepository;
            _db=db;
            _webHostEnvironment=webHostEnvironment;
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
        public ActionResult InsertCompany(Company insertCompany, IFormFile ImageFile)
        {
            if(ImageFile != null && ImageFile.Length>0)
            {
                string fileName = Path.GetFileName(ImageFile.FileName);
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }
                insertCompany.CompanyLogo = "/Images/" + fileName;
            }
            //return RedirectToAction("Index");   
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