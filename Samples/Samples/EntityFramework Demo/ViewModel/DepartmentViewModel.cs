using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.ViewModel
{
    public class DepartmentViewModel
    {
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public List<EmpoyeeDBViewModel> Employees { get; set; }
    }
}