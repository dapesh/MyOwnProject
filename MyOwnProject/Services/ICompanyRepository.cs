using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompany();
    }
}
