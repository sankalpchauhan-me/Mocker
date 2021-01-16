using EntityFramework_Demo.Database;
using EntityFramework_Demo.Models;
using EntityFramework_Demo.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EntityFramework_Demo.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController :ApiController
    {
        private EmployeeRepository _repository;

        public EmployeesController()
        {
            _repository = new EmployeeRepository();
        }
        
        // GET: Departments
        [Route("")]
        public IHttpActionResult GetAllData()
        {
            try
            {
                List<Department> fulldata = _repository.GetAllDepartment();
                List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
                foreach (Department d in fulldata) {
                    DepartmentViewModel departmentViewModel = new DepartmentViewModel()
                    {
                        DeptId = d.DeptId,
                        Name = d.Name,
                        Location = d.Location,
                    };
                    List<EmpoyeeDBViewModel> empoyeeDBViewModels = new List<EmpoyeeDBViewModel>();
                    foreach(Employee e in d.Employees)
                    {
                        EmpoyeeDBViewModel empoyeeDBViewModel = e;
                        empoyeeDBViewModels.Add(e);
                    }
                    departmentViewModel.Employees = empoyeeDBViewModels;
                    departmentViewModels.Add(departmentViewModel);
                }
                return Ok(departmentViewModels);

            } catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }
        
    }
}