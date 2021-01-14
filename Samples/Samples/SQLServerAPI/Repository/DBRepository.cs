using SQLService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SQLServerAPI.Repository
{
    public class DBRepository : IDBRepository
    {
        private readonly EmployeeDBEntities _context;
        public DBRepository()
        {
            _context = new EmployeeDBEntities();
        }
        public DBRepository(EmployeeDBEntities context)
        {
            _context = context;
        }


        public Employee Delete(int EmployeeID)
        {
            Employee employee = _context.Employees.Find(EmployeeID);
            _context.Employees.Remove(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public IEnumerable<Employee> GetByGender(string gender)
        {
            return _context.Employees.Where(e => e.Gender == gender).ToList();
        }

        public Employee GetById(int EmployeeID)
        {
            return _context.Employees.Find(EmployeeID);
        }

        public void Insert(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Employee Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            return employee;
        }
    }
}