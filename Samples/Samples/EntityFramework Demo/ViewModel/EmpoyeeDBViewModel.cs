using EntityFramework_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.ViewModel
{
    public class EmpoyeeDBViewModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public string JobTitle { get; set; }
        public int DeptId { get; set; }

        public static implicit operator EmpoyeeDBViewModel(Employee v)
        {
            return new EmpoyeeDBViewModel
            {
                FirstName = v.FirstName,
                LastName = v.LastName,
                Gender = v.Gender,
                Salary = v.Salary,
                JobTitle = v.JobTitle,
                DeptId = v.DeptId
            };
        }
    }
}