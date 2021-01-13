using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SQLServerAPI;
using SQLServerAPI.Utils;
using SQLService;

namespace SQLServerAPI.Controllers
{
    [RoutePrefix("api/employees")]
    public class SQLController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get(string gender = "All")
        {
            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Ok(entities.Employees.ToList());
                    case "male":
                        return Ok(entities.Employees.Where(e => e.Gender == "male").ToList());
                    case "female":
                        return Ok(entities.Employees.Where(e => e.Gender == "female").ToList());
                    default:
                        return BadRequest("Query gender only accepts all, male and female and not " + gender);

                }
               
            }

        }

        [Route("{id:int}", Name ="GetEmployeeById")]
        [HttpGet]
        public IHttpActionResult GetEmployees(int id)
        {

            using (EmployeeDBEntities entities = new EmployeeDBEntities())
            {
                var entity =  entities.Employees.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Ok(entity);
                }
                else
                {
                    return NotFound();
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, );
                }
            }

        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody]Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
                    //var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    //message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return Created(new Uri(Url.Link("GetEmployeeById", new { id = employee.ID })), employee);
                }
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity != null)
                    {
                        entities.Employees.Remove(entity);
                        entities.SaveChanges();
                        return Ok(entity);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            } catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        [Route("update/{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDBEntities entities = new EmployeeDBEntities())
                {
                    var entity = entities.Employees.FirstOrDefault(e => e.ID == id);
                    if(entity != null)
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Salary = employee.Salary;
                        entities.SaveChanges();
                        return Ok(entity);
                    }
                    else
                    {
                        return NotFound();

                    }
                }
            }
            catch(Exception e)
            {
                return BadRequest("" + e);
            }
        }
    }

}