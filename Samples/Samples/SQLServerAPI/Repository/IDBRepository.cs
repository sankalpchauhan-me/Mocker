using SQLService;
using System.Collections.Generic;

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
