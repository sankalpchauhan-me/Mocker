using EntityFramework_Demo.Models;
using System.Collections.Generic;
using System.Linq;

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

        public List<Department> GetAllDepartment()
        {
            return _context.Departments.Include("Employees").ToList();
        }
        public List<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }
    }
}