using Dapper;
using Microsoft.Data.SqlClient;
using MyOwnProject.Data;
using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public DepartmentRepository(IConfiguration configuration, ApplicationDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "Select * from Departments";
            using (var connection = new SqlConnection(connectionString))
            {
                var departments = await connection.QueryAsync<Department>(query);
                return departments.ToList();
            }
        }
    }
}
