using CsvApplication.BLL.Interfaces;
using CsvApplication.DAL.Entities;
using CsvApplication.DAL.Specs;
using CsvApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CsvApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IParserService _parserService;

        public HomeController(IEmployeeService employeeService, 
            IParserService parserService)
        {
            _employeeService = employeeService;
            _parserService = parserService;
        }

        public async Task<IActionResult> Update([FromBody] ICRUDModel<Employee> spec)
        {
            await _employeeService.UpdateEmployee(spec.value);
            return Json(spec.value);
        }

        [HttpPost]
        public async Task<IActionResult> ReadCsvFile(IFormFile file)
        {
            if (file.FileName.EndsWith(".csv"))
            {
                var employees = _parserService.ReadCSVFile(file);
                foreach(var employee in employees)
                {
                    await _employeeService.AddEmployee(employee);
                }
            }

            return Redirect("/Home/Index");
        }

        public async Task<ActionResult> Delete([FromBody] ICRUDModel<Employee> spec)
        {
            await _employeeService.DeleteEmployee(spec.key.ToString());
            return Json(spec);
        }

        public async Task<IActionResult> SendGrid([FromBody] DataManagerRequest dataRequest)
        {
            IEnumerable DataSource = await _employeeService.GetAllEmployees();
            var operation = new DataOperations();
            if (dataRequest.Sorted != null && dataRequest.Sorted.Count > 0)
            {
                DataSource = operation.PerformSorting(DataSource, dataRequest.Sorted);
            }
            int count = DataSource.Cast<Employee>().Count();
            if (dataRequest.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dataRequest.Skip);
            }
            if (dataRequest.Search != null && dataRequest.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dataRequest.Search); 
            }
            if (dataRequest.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dataRequest.Take);
            }
            return dataRequest.RequiresCounts ? new JsonResult(new { result = DataSource, count = count }) : new JsonResult(DataSource);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
