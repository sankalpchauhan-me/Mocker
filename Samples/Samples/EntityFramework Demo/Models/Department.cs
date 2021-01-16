using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.Models
{
    //[JsonObject(IsReference = true)]
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}