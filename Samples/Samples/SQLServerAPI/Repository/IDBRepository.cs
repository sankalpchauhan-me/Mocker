using SQLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerAPI.Repository
{
    interface IDBRepository
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetByGender(string gender);
        Employee GetById(int EmployeeID);
        void Insert(Employee employee);
        Employee Update(Employee employee);
        Employee Delete(int EmployeeID);
        void Save();
    }
}
