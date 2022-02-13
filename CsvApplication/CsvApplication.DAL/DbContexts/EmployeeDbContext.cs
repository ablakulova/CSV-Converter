using CsvApplication.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CsvApplication.DAL.DbContexts
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
