using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees(int departmentId);
    }
}
