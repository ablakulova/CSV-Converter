using CsvApplication.BLL.Interfaces;
using CsvApplication.DAL.Entities;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CsvApplication.BLL.Services
{
    public class ParserService : IParserService
    {
        public List<Employee> ReadCSVFile(IFormFile csvFile)
        {
            try
            {
                using (var streamReader = new StreamReader(csvFile.OpenReadStream(), Encoding.Default))
                using (var csvReader = new CsvReader(streamReader, new CultureInfo("en-GB")))
                {
                    csvReader.Context.RegisterClassMap<Mappings>();
                    var employeeRecords = new List<Employee>();

                    csvReader.Read();
                    csvReader.ReadHeader();

                    while (csvReader.Read())
                    {
                        employeeRecords.Add(csvReader.GetRecord<Employee>());
                    }
                    return employeeRecords;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<byte[]> WriteCSVFileAsync(IEnumerable<Employee> records)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, new CultureInfo("en-GB"));

                csvWriter.Context.RegisterClassMap<Mappings>();
                await csvWriter.WriteRecordsAsync(records);
            }

            return memoryStream.ToArray();
        }
    }
}
