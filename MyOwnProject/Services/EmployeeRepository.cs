using Dapper;
using Microsoft.Data.SqlClient;
using MyOwnProject.Data;
using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(IConfiguration configuration, ApplicationDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }
        public async Task<IEnumerable<Employee>> GetEmployees(int departmentId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "Select * from Employees where departmentId=@departmentId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@departmentId", departmentId);
            using (var connection = new SqlConnection(connectionString))
            {
                var employees = await connection.QueryAsync<Employee>(query,parameters);
                return employees.ToList();
            }
        }
    }
}
