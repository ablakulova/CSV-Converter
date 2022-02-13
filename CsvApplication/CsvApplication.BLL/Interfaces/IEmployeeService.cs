using CsvApplication.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvApplication.BLL.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeById(string Id);
        public Task DeleteEmployee(string Id);

    }
}
