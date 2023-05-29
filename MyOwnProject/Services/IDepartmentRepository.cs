using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartments(int id);
    }
}
