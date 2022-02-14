using CsvApplication.BLL.Services;
using CsvApplication.DAL.Entities;
using CsvApplication.DAL.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CsvApplication.BLL.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _employeeService = new EmployeeService(_repositoryMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnAListOfEmployees_WhenEmployeesExist()
        {
            var employees = CreateEmployeeList();

            _repositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync(employees);

            var result = await _employeeService.GetAllEmployees();

            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenEmployeesDoNotExist()
        {
            _repositoryMock.Setup(c =>
                c.GetAll()).ReturnsAsync((List<Employee>)null);

            var result = await _employeeService.GetAllEmployees();

            Assert.Null(result);
        }

        [Fact]
        public async void GetById_ShouldReturnEmployee_WhenEmployeeExist()
        {
            var employee = CreateEmployee();

            _repositoryMock.Setup(c => c.GetById(employee.Id))
                .ReturnsAsync(employee);

            var result = await _employeeService.GetEmployeeById(employee.Id);

            Assert.NotNull(result);
            Assert.IsType<Employee>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenRepositoryDoesNotExist()
        {
            _repositoryMock.Setup(c => c.GetById("1"))
                .ReturnsAsync((Employee)null);

            var result = await _employeeService.GetEmployeeById("1");

            Assert.Null(result);
        }


        private Employee CreateEmployee()
        {
            return new Employee()
            {
                Id = "1",
                PayrollNumber = "33",
                FirstName = "John",
                SurName = "Hopkins",
                DateOfBirth = DateTime.Now.AddYears(-19)
            };
        }

        private List<Employee> CreateEmployeeList()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = "1",
                    PayrollNumber = "33",
                    FirstName = "John",
                    SurName = "Hopkins",
                    DateOfBirth = DateTime.Now.AddYears(-19)
                },
                new Employee()
                {
                    Id = "2",
                    PayrollNumber = "44",
                    FirstName = "JohnX",
                    SurName = "HopkinsX",
                    DateOfBirth = DateTime.Now.AddYears(-23)
                },
                new Employee()
                {
                    Id = "1",
                    PayrollNumber = "55",
                    FirstName = "JohnY",
                    SurName = "HopkinsY",
                    DateOfBirth = DateTime.Now.AddYears(-26)
                }
             };
        }
    }
}
