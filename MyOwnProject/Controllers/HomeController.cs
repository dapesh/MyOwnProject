using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Models;
using MyOwnProject.Services;
using System.Diagnostics;

namespace MyOwnProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        public HomeController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public ActionResult Index()
        {
            var company = _companyRepository.GetCompany();
            return View(company);
        }
    }
}