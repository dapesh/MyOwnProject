using Dapper;
using Microsoft.Data.SqlClient;
using MyOwnProject.Data;
using MyOwnProject.Models;

namespace MyOwnProject.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "Select * from Companies";
            using (var connection = new SqlConnection(connectionString))
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }  
    }
}
