using CsvApplication.BLL.Interfaces;
using CsvApplication.DAL.Entities;
using CsvApplication.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvApplication.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _repository.GetAll();
        }

        public async Task<Employee> GetEmployeeById(string Id)
        {
            return await _repository.GetById(int.Parse(Id));
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _repository.Update(employee);
            await _repository.SaveAsync();

            return result;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var emp = await _repository.Add(employee);
            await _repository.SaveAsync();

            return emp;
        }

        public async Task DeleteEmployee(string id)
        {
            await _repository.Delete(int.Parse(id));
            await _repository.SaveAsync();
        }
    }
}
