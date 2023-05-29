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
        public async Task<IEnumerable<Department>> GetDepartments(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "Select * from Departments where companyid=@companyid";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@companyid", id);
            using (var connection = new SqlConnection(connectionString))
            {
                var departments = await connection.QueryAsync<Department>(query, dynamicParameters);
                return departments.ToList();
            }
        }
    }
}
