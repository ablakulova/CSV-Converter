using CsvApplication.DAL.DbContexts;
using CsvApplication.DAL.Entities;

namespace CsvApplication.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext context) : base(context)
        {
        }
    }
}
