using CsvApplication.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CsvApplication.BLL.Interfaces
{
    public interface IParserService
    {
        List<Employee> ReadCSVFile(IFormFile csvFile);
        Task<byte[]> WriteCSVFileAsync(IEnumerable<Employee> employees);
    }
}
