using EntityFramework_Demo.Models;
using System.Data.Entity;

namespace EntityFramework_Demo.Database
{
    public class EmployeeDBContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasMany(d => d.Employees).WithRequired(e => e.Department).HasForeignKey(e=>e.DeptId);
        }

    }
}