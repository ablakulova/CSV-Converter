using System;
using System.Collections.Generic;
using System.Text;

namespace CsvApplication.DAL.Entities
{
    public class Employee
    {
        public Employee()
        {
            Id = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
        public string Id { get; set; }  
        public string PayrollNumber { get; set; }
        public string FirstName { get; set; }   
        public string SurName { get; set; }  
        public DateTime DateOfBirth { get; set; }
        public int Telephone { get; set; }
        public int Mobile { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostCode { get; set; }
        public string EmailHome { get; set; }
        public DateTime StartDate { get; set; }
    }
}
