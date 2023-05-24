using MyOwnProject.Data;
using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Company> GetCompany()
        {
           return _db.Companies.ToList();
        }
    }
}
