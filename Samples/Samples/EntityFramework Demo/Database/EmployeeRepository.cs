using EntityFramework_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.Database
{
    public class EmployeeRepository
    {
        private EmployeeDBContext _context;

        public EmployeeRepository()
        {
            _context = new EmployeeDBContext();
        }

        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;
        }

        public List<Department> GetAllDepartment() {
            return _context.Departments.Include("Employees").ToList();
        }
        public List<Employee> GetAllEmployee() {
            return _context.Employees.ToList();
        }


        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(x=>x.EmpId==id);
        }

        public void InsertEmployee(Employee e)
        {
            _context.Employees.Add(e);
            _context.SaveChanges();
        }

        public void UpdateEmployee(Employee e)
        {
            Employee emplToUpdate = _context.Employees.FirstOrDefault(x => x.EmpId == e.EmpId);
            emplToUpdate.FirstName = e.FirstName;
            emplToUpdate.LastName = e.LastName;
            emplToUpdate.Gender = e.Gender;
            emplToUpdate.Salary = e.Salary;
            emplToUpdate.JobTitle = e.JobTitle;
            _context.SaveChanges();
        }

        public void DeleteEmployee(Employee e)
        {
            Employee emplToRemove= _context.Employees.FirstOrDefault(x => x.EmpId == e.EmpId);
            _context.Employees.Remove(emplToRemove);
            _context.SaveChanges();
        }
    }
}