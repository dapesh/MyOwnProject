using Microsoft.EntityFrameworkCore;
using MyOwnProject.Models;

namespace MyOwnProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Company)
                .WithMany(c => c.Departments)
                .HasForeignKey(d => d.CompanyId);
        }
        public DbSet <Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
