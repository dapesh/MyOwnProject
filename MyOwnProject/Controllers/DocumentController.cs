using Microsoft.AspNetCore.Mvc;
using MyOwnProject.Data;
using MyOwnProject.Models;
using System.Reflection.Metadata;

namespace MyOwnProject.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DocumentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var document = _db.Documents.ToList();
            return View(document);
        }

        [HttpGet]
        public IActionResult DocumentPost(int id)
        {
            var docs = _db.Documents.Find(id);
            return View(docs);
        }
        [HttpPost]
        public JsonResult DocumentPost(string title,string description)
        {
            return Json("");
        }
    }
}
