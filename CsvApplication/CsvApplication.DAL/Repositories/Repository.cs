using CsvApplication.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvApplication.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EmployeeDbContext _context;
        public Repository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var emp = await _context.Set<T>().FindAsync(id);
            if (emp != null)
            {
                _context.Set<T>().Remove(emp);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var employees = await _context.Set<T>().ToListAsync();
            return employees;
        }

        public async Task<T> GetById(int id)
        {
            var employee = await _context.Set<T>().FindAsync(id);
            return employee;
        }

        public async Task<T> Update(T entity)
        {
            var result = await _context.Set<T>().FindAsync(entity);
            return result;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
