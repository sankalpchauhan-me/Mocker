using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}