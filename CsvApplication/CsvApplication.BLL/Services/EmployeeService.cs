using CsvApplication.BLL.Interfaces;
using CsvApplication.DAL.DbContexts;
using CsvApplication.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvApplication.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeService(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(string Id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(a => a.Id == Id);
            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (result != null)
            {
                result.PayrollNumber = employee.PayrollNumber;
                result.FirstName = employee.FirstName;
                result.SurName = employee.SurName;
                result.DateOfBirth = employee.DateOfBirth;
                result.Telephone = employee.Telephone;
                result.Mobile = employee.Mobile;
                result.Address = employee.Address;
                result.Address2 = employee.Address2;
                result.PostCode = employee.PostCode;
                result.EmailHome = employee.EmailHome;
                result.StartDate = employee.StartDate;

                await _dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var emp = await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();

            return emp.Entity;
        }

        public async Task DeleteEmployee(string Id)
        {
            var emp = await _dbContext.Employees.FirstOrDefaultAsync(a => a.Id == Id);
            if (emp != null)
            {
                _dbContext.Employees.Remove(emp);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
