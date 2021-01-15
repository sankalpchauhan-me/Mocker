using EntityFramework_Demo.Database;
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
                return Ok(_repository.GetAllDepartment());
            } catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }
        
    }
}