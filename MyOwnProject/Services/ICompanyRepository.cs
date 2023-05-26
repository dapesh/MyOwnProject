using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
    }
}
