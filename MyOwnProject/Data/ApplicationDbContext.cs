using Microsoft.EntityFrameworkCore;
using MyOwnProject.Models;

namespace MyOwnProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet <Company> Companies { get; set; }
    }
}
